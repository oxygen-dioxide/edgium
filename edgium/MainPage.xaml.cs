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
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace edgium
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string DefaultHomePage="https://bing.com/";

        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void TabView_Loaded(object sender, RoutedEventArgs e)
        {
            AddNewTab(DefaultHomePage);
        }

        private void TabView_AddButtonClick(TabView sender, object args)
        {
            AddNewTab(DefaultHomePage);
        }

        private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }

        private TabViewItem CreateNewTab(string url)
        {
            var newTab = new muxc.TabViewItem();
            newTab.IconSource = new muxc.SymbolIconSource() { Symbol = Symbol.Document };
            newTab.Header = "New Document";

            // The Content of a TabViewItem is often a frame which hosts a page.
            Frame frame = new Frame();
            newTab.Content = frame;
            frame.Navigate(typeof(BrowserTab),url);
            //new MessageDialog(MyTabView.Height.ToString(), "消息提示").ShowAsync();
            return newTab;
        }
        public void AddNewTab(string url)
        {
            MyTabView.TabItems.Add(CreateNewTab(url));
        }

    }
}
