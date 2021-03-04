using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Web.WebView2.Core;
using Windows.ApplicationModel.DataTransfer;
using muxc = Microsoft.UI.Xaml.Controls;
using Windows.System;
using Windows.UI.Core;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace edgium
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BrowserTab : Page
    {
        public BrowserTab()
        {
            this.InitializeComponent();
            MyWebView.NavigationStarting += onNavigationStarting;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //将传过来的数据 类型转换一下
            navigate((string)e.Parameter);
        }
        private void navigate(string url)
        {
            try
            {
                MyWebView.Source = new Uri(url);
            }
            catch (FormatException ex)
            {
                // Incorrect address entered.
            }
        }
        private void go()
        {
            navigate(addressBar.Text);
        }
        private void back()//后退
        {
            MyWebView.GoBack();
        }
        private void back_click(object sender, RoutedEventArgs e)//后退按钮事件处理
        {
            back();
        }
        private void forward()//前进
        {
            MyWebView.GoForward();
        }
        private void forward_click(object sender, RoutedEventArgs e)//前进按钮事件处理
        {
            forward();
        }
        private void refresh()//刷新
        {
            MyWebView.Reload();
        }
        private void refresh_click(object sender, RoutedEventArgs e)//刷新按钮事件处理
        {
            refresh();
        }
        private void newtab(object sender, RoutedEventArgs e)//新建标签页
        {
            
        }
        private void newwindow(object sender, RoutedEventArgs e)//新建窗口
        {

        }
        private void onNavigationStarting(WebView2 sender, CoreWebView2NavigationStartingEventArgs args)
        //网址跳转时修改地址栏
        {
            String uri = args.Uri;
            addressBar.Text = uri;
        }
        private void IndexPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            //分享一个链接
            string shareLinkString = addressBar.Text;
            //创建一个数据包
            DataPackage dataPackage = new DataPackage();
            //把要分享的链接放到数据包里
            dataPackage.SetWebLink(new Uri(shareLinkString));
            //数据包的标题（内容和标题必须提供）
            dataPackage.Properties.Title = "Title";
            //数据包的描述
            dataPackage.Properties.Description = "Description";
            //给dataRequest对象赋值
            DataRequest request = args.Request;
            request.Data = dataPackage;
        }
        private void share(object sender, RoutedEventArgs e)//分享
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += IndexPage_DataRequested;

            //展示系统的共享ui
            DataTransferManager.ShowShareUI();
        }
        private void print()//打印
        {
            MyWebView.ExecuteScriptAsync($"window.print()");
        }
        private void print_click(object sender, RoutedEventArgs e)//打印
        {
            print();
        }
        private static bool IsCtrlKeyPressed()
        {
            var ctrlState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Control);
            return (ctrlState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
        }
        private static bool IsAltKeyPressed()
        {
            var altState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Menu);
            return (altState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
        }
        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)//处理快捷键
        {
            if (IsCtrlKeyPressed())
            {
                switch (e.Key)
                {
                    case VirtualKey.P: print(); break;
                    case VirtualKey.R: refresh(); break;
                }
            }
            if(IsAltKeyPressed())
            {
                switch(e.Key)
                {
                    case VirtualKey.Left: back(); break;
                    case VirtualKey.Right: forward(); break;
                }
            }
        }
        private void addressBar_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case VirtualKey.Enter:go();break;
            }
        }
    }
}
