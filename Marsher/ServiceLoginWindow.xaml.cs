using CefSharp;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Marsher
{
    /// <summary>
    /// ServiceLoginWindow.xaml 的交互逻辑
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantDefaultMemberInitializer")]
    public partial class ServiceLoginWindow
    {
        public CookieContainer ResultContainer = null;

        public ServiceLoginWindow()
        {
            InitializeComponent();
        }

        public void Initialize(Uri browserUri, Uri cookiesUri, string title)
        {
            ChromeBrowser.LoadUrl(browserUri.ToString());
            //_chromeBrowser = new ChromiumWebBrowser(browserUri.ToString());
            //BrowserPanel.Children.Add(_chromeBrowser);
            //_chromeBrowser.HorizontalAlignment = HorizontalAlignment.Stretch;
            //_chromeBrowser.VerticalAlignment = VerticalAlignment.Stretch;
            ChromeBrowser.AddressChanged += _chromeBrowser_AddressChanged;
            Title = title;
        }

        public void Initialize(Uri browserUri, string title)
        {
            ChromeBrowser.LoadUrl(browserUri.ToString());
            ChromeBrowser.AddressChanged += _chromeBrowser_AddressChanged;
            Title = title;
        }

        private void _chromeBrowser_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //noting
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            ResultContainer = GetUriCookieContainer();
            Close();
        }

        private CookieContainer GetUriCookieContainer()
        {
            var container = new CookieContainer();
            using (var cookiemanager = ChromeBrowser.GetCookieManager())
            using (var visitor = new TaskCookieVisitor())
            {
                cookiemanager.VisitAllCookies(visitor);
                var list = visitor.Task.GetAwaiter().GetResult();
                list.ForEach(c =>
                {
                    container.Add(new System.Net.Cookie(c.Name, c.Value, c.Path, c.Domain));
                });
            }
            return container;
        }
    }
}
