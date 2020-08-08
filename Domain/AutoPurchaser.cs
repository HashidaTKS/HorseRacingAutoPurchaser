using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace HorseRacingAutoPurchaser
{
    //全体的に、Thread.Sleep()はやめたい
    public class AutoPurchaser : IDisposable
    {
        private ChromeDriver Chrome { get; }

        private LoginConfig LoginConfig { get; }

        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                Chrome.Quit();
                Chrome.Dispose();
            }
            disposed = true;
        }

        public AutoPurchaser(LoginConfig loginConfig)
        {
            var chromeOptions = new ChromeOptions();
            Chrome = new ChromeDriver(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), chromeOptions);
            LoginConfig = loginConfig;
        }

        public bool Purchase(List<BetDatum> betInfoList){
            var groupedBetInfoList = betInfoList.GroupBy(_ => _.RaceData.HoldingDatum.Region.RagionType);
            foreach(var group in groupedBetInfoList)
            {
                if(group.Key == RegionType.Central)
                {
                    if (!PurchaseAtNetKeiba(group.ToList()))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!PurchaseAtRakutenKeiba(group.ToList()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 現状は馬連のみに対応。ある一つのレースへの複数のベットを行う。
        /// </summary>
        /// <param name="betInfoList"></param>
        private bool PurchaseAtNetKeiba(List<BetDatum> betInfoList)
        {
            try
            {
                var firstBetInfo = betInfoList.FirstOrDefault();
                if(firstBetInfo == null)
                {
                    return true;
                }
                var url = firstBetInfo.RaceData.ToNetKeibaIpatPageUrlString();
                Chrome.Url = url;
                //画面の切り替わり完了待ち
                Thread.Sleep(1 * 1000);

                LoginToNetkeibaIfNeeded(Chrome);

                var count = 1;
                foreach (var betInfo in betInfoList)
                {
                    var shikibetu = Chrome.FindElementByClassName("shikibetu");
                    var selectedShikibetu = shikibetu.FindElement(By.LinkText("馬連"));
                    selectedShikibetu.Click();

                    var houshiki = Chrome.FindElementByClassName("houshiki");
                    var selectedHoushiki = houshiki.FindElement(By.LinkText("通常"));
                    selectedHoushiki.Click();
                    var inputTable = Chrome.FindElementByClassName("IpatTable");

                    var selectedBlock = Chrome.FindElementByClassName("Selected_Block");
                    var kaimeInput = selectedBlock.FindElement(By.Name("money"));
                    var ticketAddButton = selectedBlock.FindElement(By.ClassName("AddBtn"));

                    foreach (var uma in betInfo.HorseNumList)
                    {
                        var id = $"uc-0-{uma}";
                        var checkTarget = inputTable.FindElement(By.Id(id)).FindElement(By.XPath(".."));
                        checkTarget.Click();
                    }
                    var sendNum = betInfo.BetMoney / 100;
                    kaimeInput.SendKeys(sendNum.ToString());
                    ticketAddButton.Click();
                    count += 1;
                }

                Chrome.FindElementById("ipat_dialog").Click();
                Thread.Sleep(3 * 1000);

                var frameElement = Chrome.FindElementByClassName("cboxIframe");
                Chrome.SwitchTo().Frame(frameElement);

                GoNextIfIpatCooperationDialogIsDisplayed(Chrome);
                LoginToIpat(Chrome);
                return PurchaseAtIpat(Chrome, betInfoList);
            }
            catch
            {
                Console.WriteLine("Could not Purchace.");
                return false;
            }
        }

        private void GoNextIfIpatCooperationDialogIsDisplayed(ChromeDriver chrome)
        {
            try
            {
                var kiyakuForm = chrome.FindElementByName("kiyaku_form");
                var acceptButtonParent = kiyakuForm.FindElement(By.ClassName("Agree"));
                var acceptButton = acceptButtonParent.FindElement(By.TagName("input"));
                acceptButton.Click();
                Thread.Sleep(1 * 1000);
            }
            catch (Exception ex)
            {
                //nothin to do
            }
        }

        private void LoginToIpat(ChromeDriver chrome)
        {
            try
            {
                var loginFormCollection = chrome.FindElementsByClassName("Ipat_Login_Form");
                var subscriberForm = loginFormCollection[0].FindElement(By.TagName("input"));
                var passwordForm = loginFormCollection[1].FindElement(By.TagName("input")); 
                var P_ArsForm = loginFormCollection[2].FindElement(By.TagName("input")); 

                subscriberForm.SendKeys(LoginConfig.JRA_SubscriberNumber);
                passwordForm.SendKeys(LoginConfig.JRA_LoginPassword);
                P_ArsForm.SendKeys(LoginConfig.JRA_P_ARS);

                var submitButton = chrome.FindElementByClassName("SubmitBtn");
                Thread.Sleep(500);

                submitButton.Click();
                //結構時間がかかるケースがあるので、結構待つ。
                Thread.Sleep(8000);
            }
            catch (Exception ex)
            {
                //nothin to do
            }
        }

        private bool PurchaseAtIpat(ChromeDriver chrome, List<BetDatum> betInfoList)
        {
            try
            {
                var sumForm = chrome.FindElementById("sum");
                var sum = betInfoList.Sum(_ => _.BetMoney);
                sumForm.SendKeys(sum.ToString());

                var submitButton = chrome.FindElementByLinkText("投票");

                submitButton.Click();
                Thread.Sleep(1000);

                var alert = chrome.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(1000);

                var logoutButton = chrome.FindElementByLinkText("ログアウト");
                logoutButton.Click();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not Purchace.");
                Console.WriteLine(ex);
                return false;
            }
        }

        private bool PurchaseAtRakutenKeiba(List<BetDatum> betInfoList)
        {
            var url = "https://bet.keiba.rakuten.co.jp/bet_lite";

            Chrome.Url = url;

            //画面の切り替わり完了待ち
            Thread.Sleep(1 * 1000);

            LoginToRakutenIfNeeded(Chrome);

            foreach (var betInfo in betInfoList)
            {
                try
                {
                    var contents = Chrome.FindElementById("contents");

                    var racecourseId = Chrome.FindElementByName("racecourseId");
                    new SelectElement(racecourseId).SelectByText(betInfo.RaceData.HoldingDatum.Region.RegionName);

                    var raceNumber = Chrome.FindElementByName("raceNumber");
                    new SelectElement(raceNumber).SelectByValue(betInfo.RaceData.RaceNumber.ToString());

                    var betType = Chrome.FindElementByName("betType");
                    new SelectElement(betType).SelectByText(Utility.TicketTypeToRakutenBetTypeString[betInfo.TicketType]);

                    var betMode = Chrome.FindElementByName("betMode");
                    new SelectElement(betMode).SelectByText("通常");

                    var selectButton = Chrome.FindElementByName("select");
                    selectButton.Click();

                    Thread.Sleep(1000);

                    var count = 1;
                    var voteTableTrList = Chrome.FindElementByClassName("voteTable").FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));

                    foreach (var num in betInfo.HorseNumList)
                    {
                        var baseTd = voteTableTrList[num - 1];
                        var target = baseTd.FindElement(By.Name($"me{count}[]"));
                        target.Click();
                        count++;
                    }

                    var confirmInput = Chrome.FindElementByName($"buyUnitCount");
                    var sendNum = betInfo.BetMoney / 100;
                    confirmInput.SendKeys(sendNum.ToString());

                    var confirmButton = Chrome.FindElementByName("confirm");
                    confirmButton.Click();

                    Thread.Sleep(1000);

                    var totalConfirmInput = Chrome.FindElementById("cashConfirm");
                    totalConfirmInput.SendKeys(betInfo.BetMoney.ToString());

                    var completeButton = Chrome.FindElementById("completeBtn");
                    completeButton.Click();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not Purchace.");
                    Console.WriteLine(ex);
                    //これ。途中まで成功していると重複購入する可能性がある。よくない。
                    return false;

                }
            }
            return true;
        }

        private void LoginToRakutenIfNeeded(ChromeDriver chrome)
        {
            try
            {
                var loginUser = chrome.FindElementById("loginInner_u");
                var loginPassword = chrome.FindElementById("loginInner_p");
                var loginButton = chrome.FindElementByClassName("loginButton");

                loginUser.SendKeys(LoginConfig.RakutenId);
                loginPassword.SendKeys(LoginConfig.RakutenPassword);
                Thread.Sleep(1 * 1000);
                loginButton.Click();
                Thread.Sleep(1 * 1000);

            }
            catch (Exception ex)
            {
                //Do not need to login
                //nothin to do
            }
        }

        private void LoginToNetkeibaIfNeeded(ChromeDriver chrome)
        {
            try
            {
                var loginBox = chrome.FindElementByName("loginbox");
                var loginId = loginBox.FindElement(By.Name("login_id"));
                var loginPassword = loginBox.FindElement(By.Name("pswd"));
                var loginButton = loginBox.FindElement(By.Name("ログイン"));
                loginId.SendKeys(LoginConfig.NetkeibaId);
                loginPassword.SendKeys(LoginConfig.NetkeibaPassword);
                Thread.Sleep(1 * 1000);
                loginButton.Click();
                Thread.Sleep(1 * 1000);
            }
            catch (Exception ex)
            {
                //Do not need to login
                //nothin to do
            }
        }

    }
}
