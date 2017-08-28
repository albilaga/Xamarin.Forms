﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Embedding.XF;
using Xamarin.Forms.Platform.Android;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using View = Android.Views.View;
using Button = Android.Widget.Button;

namespace Embedding.Droid
{
	[Activity(Label = "SecondActivity")]
	public class SecondActivity : FragmentActivity
	{
		Fragment _hello;
		Fragment _alertsAndActionSheets;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView (Resource.Layout.Second);
			
			var ft = SupportFragmentManager.BeginTransaction();
			ft.Replace(Resource.Id.fragment_frame_layout, new SecondFragment(), "main");
			ft.Commit();
		}

		void ShowEmbeddedPageFragment(Fragment fragment)
		{
			FragmentTransaction ft = SupportFragmentManager.BeginTransaction();

			ft.AddToBackStack(null);
			ft.Replace(Resource.Id.fragment_frame_layout, fragment, "hello");
			
			ft.Commit();
		}

		public void ShowHello()
		{
			// Create a XF Hello page as a fragment
			if (_hello == null)
			{
				_hello = new Hello().CreateSupportFragment(this);
			}

			ShowEmbeddedPageFragment(_hello);
		}

		public void ShowAlertsAndActionSheets()
		{
			if (_alertsAndActionSheets== null)
			{
				_alertsAndActionSheets = new AlertsAndActionSheets().CreateSupportFragment(this);
			}

			ShowEmbeddedPageFragment(_alertsAndActionSheets);
		}

	}

	public class SecondFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view =  inflater.Inflate(Resource.Layout.SecondFragment, container, false);
			var showEmbeddedButton = view.FindViewById<Button>(Resource.Id.showEmbeddedButton);
			var showAlertsActionSheets = view.FindViewById<Button>(Resource.Id.showAlertsActionSheets);

			showEmbeddedButton.Click += ShowEmbeddedClick;
			showAlertsActionSheets.Click += ShowAlertsActionSheetsClick;

			return view;
		}

		void ShowAlertsActionSheetsClick(object sender, EventArgs eventArgs)
		{
			((SecondActivity)Activity).ShowAlertsAndActionSheets();
		}

		void ShowEmbeddedClick(object sender, EventArgs e)
		{
			((SecondActivity)Activity).ShowHello();
		}
	}
}