using Android.App;
using Android.Widget;
using Android.OS;
using SALLab11;

namespace Lab11
{
    [Activity(Label = "Lab11", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Complex data;
        int counter = 0;
        string message = string.Empty;
        TextView MessageText;

        protected override void OnCreate(Bundle bundle)
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnCreate");
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            MessageText = FindViewById<TextView>(Resource.Id.MessageText);
            var startActivityButton = FindViewById<Button>(Resource.Id.StartActivityButton);
            var clicsCounterButton = FindViewById<Button>(Resource.Id.ClicsCounterButton);

            startActivityButton.Click += (sender, e) =>
            {
                var activityIntent = new Android.Content.Intent(this, typeof(SecondActivity));
                StartActivity(activityIntent);
            };

            data = (Complex)this.FragmentManager.FindFragmentByTag("Data");
            if (data == null)
            {
                data = new Complex();
                var fragmentTransaction = this.FragmentManager.BeginTransaction();
                fragmentTransaction.Add(data, "Data");
                fragmentTransaction.Commit();
            }

            if (bundle != null)
            {
                counter = bundle.GetInt("CounterValue", 0);
                Android.Util.Log.Debug("Lab11Log", "Activity A - Recovered Instance State");
            }

            clicsCounterButton.Text = Resources.GetString(Resource.String.ClicksCounter_Text, counter);
            clicsCounterButton.Text += $"\n{data.ToString()}";
            clicsCounterButton.Click += (sender, e) =>
            {
                counter++;
                clicsCounterButton.Text = Resources.GetString(Resource.String.ClicksCounter_Text, counter);
                data.Real++;
                data.Imaginary++;
                clicsCounterButton.Text += $"\n{data.ToString()}";
            };

            if (bundle == null)
            {
                Validate();
            }
            else
            {
                message = bundle.GetString("Message", string.Empty);
                MessageText.Text = message;
            }
        }

        private async void Validate()
        {
            var serviceClient = new ServiceClient();
            var studentEmail = "jzuluaga55@gmail.com";
            var password = "Roger1974";
            var myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, 
                Android.Provider.Settings.Secure.AndroidId);
            var result = await serviceClient.ValidateAsync(studentEmail, password, myDevice);
            message = $"{result.Status}\n{result.Fullname}\n{result.Token}";
            MessageText.Text = message;
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("CounterValue", counter);
            outState.PutString("Message", message);
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnSaveInstanceState");
            base.OnSaveInstanceState(outState);
        }

        protected override void OnStart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStart");
            base.OnStart();
        }

        protected override void OnResume()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnResume");
            base.OnResume();
        }

        protected override void OnPause()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnPause");
            base.OnPause();
        }

        protected override void OnStop()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStop");
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnDestroy");
            base.OnDestroy();
        }

        protected override void OnRestart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnRestart");
            base.OnRestart();
        }
    }
}