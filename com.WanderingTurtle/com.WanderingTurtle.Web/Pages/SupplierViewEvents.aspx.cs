﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.Web
{
    public partial class SupplierViewEvents : System.Web.UI.Page
    {
        EventManager _myManager = new EventManager();
        List<Event> _listedEvents;
        public List<EventType> _eventTypes;
        public string current;
        public string transport;
        public string onsite;
        public string nameError = "";
        public string descError = "";

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                try
                {
                    _eventTypes = _myManager.RetrieveEventTypeList();
                }
                catch (Exception)
                {
                    Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Retrieving Event Types\")</SCRIPT>");
                }
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

            }
        }
        private void bindData()
        {
            lvEvents.DataSource = _listedEvents;
            lvEvents.DataBind();
        }
        public IEnumerable<Event> GetEvents()
        {
            try
            {
                _listedEvents = _myManager.RetrieveEventList();
                foreach (Event x in _listedEvents)
                {
                    //sets string values for trasportation and onsite properties
                    x.setFields();
                }
                return _listedEvents;
            }
            catch (Exception)
            {

                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Retrieving List\")</SCRIPT>");
                return null;
            }
        }
        public void FormValidate(int eventItemID)
        {
           
        }
        public void UpdateEvent(int eventItemID)
        {
            bool stop = false;
            int errorCount = 0;
            try
            {
                Event myEvent = _listedEvents
                .Where(ev => ev.EventItemID == eventItemID).FirstOrDefault();

                Event newEvent = new Event(myEvent.EventItemID, myEvent.EventItemName, myEvent.Transportation, myEvent.EventTypeID, myEvent.OnSite, myEvent.ProductID, myEvent.Description, myEvent.Active);

                if (Validator.ValidateAlphaNumeric(String.Format("{0}", Request.Form["name"]).Trim(), 1, 255))
                {
                    newEvent.EventItemName = String.Format("{0}", Request.Form["name"]).Trim();

                }
                else
                {
                    errorCount++;
                    stop = true;
                    showError("You must enter a valid Event Name!");
                    return;
                    

                }
                //created programatically so don't need to be validated
                newEvent.EventTypeID = int.Parse(Request.Form["type"]);
                newEvent.Transportation = bool.Parse(Request.Form["transport"]);
                newEvent.OnSite = bool.Parse(Request.Form["onsite"]);
                if (Validator.ValidateAlphaNumeric(String.Format("{0}", Request.Form["description"]).Trim(), 1, 255))
                {
                    newEvent.Description = String.Format("{0}", Request.Form["description"]).Trim();
                }
                else
                {
                    errorCount++;
                    stop = true;
                    showError("You must enter a valid Description!");
                    return;
                }

               
                    lblError.Text = "";
                    int result = 0;
                    if (myEvent != null)
                    {
                        result = _myManager.EditEvent(myEvent, newEvent);
                        return;
                    }
                


            }
            catch (Exception ex)
            {
                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Updating Event" + ex.Message + "\" )</SCRIPT>");
            }

        }
        /// <summary>
        /// Matt Lapka
        /// Created 2015/03/07
        /// 
        /// Sets the error message displayed on the page.
        /// </summary>
        /// <param name="errorMessage"></param>
        private void showError(string errorMessage)
        {
            lblError.Text = errorMessage;
        }
        public void DeleteEvent(int eventItemID)
        {
            try
            {
                Event myEvent = _listedEvents
                .Where(e => e.EventItemID == eventItemID).FirstOrDefault();

                int result = _myManager.ArchiveAnEvent(myEvent);
            }
            catch (Exception)
            {

                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Deleting Event\")</SCRIPT>");
            }
        }
        public void InsertEvent()
        {

        }

        protected void lvEvents_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //this.dpEvents.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData();
        }
        
        protected void lvEvents_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
                if (!Validator.ValidateAlphaNumeric(e.NewValues["EventItemName"].ToString(), 1, 255))
                {
                    Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"You must ender a valid name!\")</SCRIPT>");
                    e.Cancel = true;
                }
            
        }
    }

}