using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mercy.Models;
using Mercy.Library;
using System.IO;

namespace MercyDroidExample
{
    [Activity(Label = "MercyDroidExample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate 
            {
                button.Text = string.Format("{0} clicks!", count++);
                string root = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}wwwroot";

                var server = new MercyServer();
                server.UseDefaultBuilder();
                server.UseDefaultReporter();
                server.UseDefaultRecorder(recordIncoming: true);
                server.UsePort(12222);

                var app = new App();
                app.UseDefaultHeaders(serverName: "Mercy", keepAlive: true);
                app.UseDefaultFile("index.html");
                app.UseStaticFile(rootPath: root);
                app.UseMvc();
                app.UseNotFound(root, "404.html");

                var condition = new AppCondition();
                condition.UseDomainCondition("*");

                server.Bind(when: condition, run: app);
                server.Start().Wait();
            };
        }
    }
}

