using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FitnessApp
{
    public class SharedPrefManager
    {

        private static SharedPrefManager mInstance;
        private static Context mCtx;

        private const string SHARED_PREF_NAME = "FitnessAnon";
        private const string KEY_USERNAME = "UserName";
        private const string KEY_USERID = "UserID";
        private const string KEY_USEREMAIL = "UserEmail";
        private const string KEY_HEIGHT = "height";
        private const string KEY_FIRSTNAME = "firstName";
        private const string KEY_LASTNAME = "lastName";
        private const string KEY_AGE = "age";



        public SharedPrefManager(Context context)
        {
            mCtx = context;
        }

        public static SharedPrefManager getInstance(Context context)
        {
            lock (typeof(SharedPrefManager))
            {
                if (mInstance == null)
                {
                    mInstance = new SharedPrefManager(context);

                }
                return mInstance;
            }
        }

        internal object GetString(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public virtual bool userLogin(string UserID, string UserEmail)
        {
            ISharedPreferences sharedPreferences = mCtx.GetSharedPreferences(SHARED_PREF_NAME,FileCreationMode.Private);

            ISharedPreferencesEditor editor = sharedPreferences.Edit();
            editor.PutString(KEY_USERID, UserID);
            
            editor.PutString(KEY_USEREMAIL, UserEmail);
            

            editor.Apply();

            return true;
        }

        public virtual bool UserProfileSetup(string lastName, string firstName, string age, string height)
        {
            ISharedPreferences sharedPreferences = mCtx.GetSharedPreferences(SHARED_PREF_NAME, FileCreationMode.Private);

            ISharedPreferencesEditor editor = sharedPreferences.Edit();
            editor.PutString(KEY_LASTNAME, lastName);
            editor.PutString(KEY_FIRSTNAME, firstName);
            editor.PutString(KEY_AGE, age);
            editor.PutString(KEY_HEIGHT, height);

            editor.Apply();

            return true;
        }

        public virtual bool LoggedIn
        {
            get
            {
                ISharedPreferences sharedPreferences = mCtx.GetSharedPreferences(SHARED_PREF_NAME,FileCreationMode.Private);
                if (sharedPreferences.GetString(KEY_USERID, null) != null)
                {
                    return true;
                }
                return false;
            }
        }

        public virtual bool logout()
        {
            ISharedPreferences sharedPreferences = mCtx.GetSharedPreferences(SHARED_PREF_NAME,FileCreationMode.Private);
            ISharedPreferencesEditor editor = sharedPreferences.Edit();

            editor.Clear();
            editor.Apply();
            return true;
        }

        public virtual string UserID
        {
            get
            {
                ISharedPreferences sharedPreferences = mCtx.GetSharedPreferences(SHARED_PREF_NAME,FileCreationMode.Private);

                return sharedPreferences.GetString(KEY_USERID, "0");

            }
        }

        public virtual string Height
        {
            get
            {
                ISharedPreferences sp = mCtx.GetSharedPreferences(SHARED_PREF_NAME, FileCreationMode.Private);

                return sp.GetString(KEY_HEIGHT, "0");
            }
        }

        public virtual string FirstName
        {
            get
            {
                ISharedPreferences sp = mCtx.GetSharedPreferences(SHARED_PREF_NAME, FileCreationMode.Private);
                return sp.GetString(KEY_FIRSTNAME, "");
            }
        }

       


        public virtual string UserName
        {
            get
            {
                ISharedPreferences sharedPreferences = mCtx.GetSharedPreferences(SHARED_PREF_NAME,FileCreationMode.Private);

                return sharedPreferences.GetString(KEY_USERNAME, null);

            }
        }

        public virtual string UserEmail
        {
            get
            {
                ISharedPreferences sharedPreferences = mCtx.GetSharedPreferences(SHARED_PREF_NAME,FileCreationMode.Private);

                return sharedPreferences.GetString(KEY_USEREMAIL, null);

            }
        }

        public virtual bool userModify(string UserEmail, int RegionID)
        {
            ISharedPreferences sharedPreferences = mCtx.GetSharedPreferences(SHARED_PREF_NAME,FileCreationMode.Private);

            ISharedPreferencesEditor editor = sharedPreferences.Edit();
            editor.PutString(KEY_USEREMAIL, UserEmail);
            

            editor.Apply();

            return true;
        }



    }
}