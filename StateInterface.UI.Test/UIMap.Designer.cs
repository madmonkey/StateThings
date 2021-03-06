﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 12.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace StateInterface.UI.Test
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public partial class UIMap
    {
        
        /// <summary>
        /// ExpandFirstForm
        /// </summary>
        public void ExpandFirstForm()
        {
            #region Variable Declarations
            HtmlHyperlink uIItemHyperlink = this.UIHomePageInternetExplWindow.UICertificationUpdateDocument.UIAccordian_FREEPane.UIItemHyperlink;
            #endregion

            // Wait for 5 seconds for user delay between actions; Click link
            Playback.Wait(5000);
            Mouse.Click(uIItemHyperlink, new Point(10, 8));
        }
        
        /// <summary>
        /// RecordedMethod1
        /// </summary>
        public void RecordedMethod1()
        {
            #region Variable Declarations
            HtmlHyperlink uIDesignHyperlink = this.UIHomePageInternetExplWindow.UIHomePageDocument.UIDesignHyperlink;
            HtmlHyperlink uIUpdateCertificationHyperlink = this.UIHomePageInternetExplWindow.UICertifyDocument.UIUpdateCertificationHyperlink;
            HtmlHyperlink uIGetFormsHyperlink = this.UIHomePageInternetExplWindow.UICertificationUpdateDocument.UIGetFormsHyperlink;
            #endregion

            // Click 'Design' link
            Mouse.Click(uIDesignHyperlink, new Point(82, 78));

            // Click 'Update Certification' link
            Mouse.Click(uIUpdateCertificationHyperlink, new Point(67, 50));

            // Click 'Get Forms' link
            Mouse.Click(uIGetFormsHyperlink, new Point(20, 7));
        }
        
        #region Properties
        public UIHomePageInternetExplWindow UIHomePageInternetExplWindow
        {
            get
            {
                if ((this.mUIHomePageInternetExplWindow == null))
                {
                    this.mUIHomePageInternetExplWindow = new UIHomePageInternetExplWindow();
                }
                return this.mUIHomePageInternetExplWindow;
            }
        }
        #endregion
        
        #region Fields
        private UIHomePageInternetExplWindow mUIHomePageInternetExplWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public class UIHomePageInternetExplWindow : BrowserWindow
    {
        
        public UIHomePageInternetExplWindow()
        {
            #region Search Criteria
            this.SearchProperties[UITestControl.PropertyNames.Name] = "Home Page";
            this.SearchProperties[UITestControl.PropertyNames.ClassName] = "IEFrame";
            this.WindowTitles.Add("Home Page");
            this.WindowTitles.Add("Certify");
            this.WindowTitles.Add("Certification Update");
            #endregion
        }
        
        public void LaunchUrl(System.Uri url)
        {
            this.CopyFrom(BrowserWindow.Launch(url));
        }
        
        #region Properties
        public UIHomePageDocument UIHomePageDocument
        {
            get
            {
                if ((this.mUIHomePageDocument == null))
                {
                    this.mUIHomePageDocument = new UIHomePageDocument(this);
                }
                return this.mUIHomePageDocument;
            }
        }
        
        public UICertifyDocument UICertifyDocument
        {
            get
            {
                if ((this.mUICertifyDocument == null))
                {
                    this.mUICertifyDocument = new UICertifyDocument(this);
                }
                return this.mUICertifyDocument;
            }
        }
        
        public UICertificationUpdateDocument UICertificationUpdateDocument
        {
            get
            {
                if ((this.mUICertificationUpdateDocument == null))
                {
                    this.mUICertificationUpdateDocument = new UICertificationUpdateDocument(this);
                }
                return this.mUICertificationUpdateDocument;
            }
        }
        #endregion
        
        #region Fields
        private UIHomePageDocument mUIHomePageDocument;
        
        private UICertifyDocument mUICertifyDocument;
        
        private UICertificationUpdateDocument mUICertificationUpdateDocument;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public class UIHomePageDocument : HtmlDocument
    {
        
        public UIHomePageDocument(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[HtmlDocument.PropertyNames.Id] = null;
            this.SearchProperties[HtmlDocument.PropertyNames.RedirectingPage] = "False";
            this.SearchProperties[HtmlDocument.PropertyNames.FrameDocument] = "False";
            this.FilterProperties[HtmlDocument.PropertyNames.Title] = "Home Page";
            this.FilterProperties[HtmlDocument.PropertyNames.AbsolutePath] = "/sungardstateinterfacetest";
            this.FilterProperties[HtmlDocument.PropertyNames.PageUrl] = "http://psjdevtest/sungardstateinterfacetest";
            this.WindowTitles.Add("Home Page");
            #endregion
        }
        
        #region Properties
        public HtmlHyperlink UIDesignHyperlink
        {
            get
            {
                if ((this.mUIDesignHyperlink == null))
                {
                    this.mUIDesignHyperlink = new HtmlHyperlink(this);
                    #region Search Criteria
                    this.mUIDesignHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Id] = null;
                    this.mUIDesignHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Name] = null;
                    this.mUIDesignHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Target] = null;
                    this.mUIDesignHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.InnerText] = null;
                    this.mUIDesignHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.AbsolutePath] = "/SunGardStateInterfaceTest/Certify";
                    this.mUIDesignHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Title] = "Design";
                    this.mUIDesignHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Href] = "http://psjdevtest/SunGardStateInterfaceTest/Certify";
                    this.mUIDesignHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Class] = "dashboardIcon glyphicon glyphicon-ok glyphicon-white";
                    this.mUIDesignHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.ControlDefinition] = "title=\"Design\" class=\"dashboardIcon glyp";
                    this.mUIDesignHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.TagInstance] = "7";
                    this.mUIDesignHyperlink.WindowTitles.Add("Home Page");
                    #endregion
                }
                return this.mUIDesignHyperlink;
            }
        }
        #endregion
        
        #region Fields
        private HtmlHyperlink mUIDesignHyperlink;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public class UICertifyDocument : HtmlDocument
    {
        
        public UICertifyDocument(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[HtmlDocument.PropertyNames.Id] = null;
            this.SearchProperties[HtmlDocument.PropertyNames.RedirectingPage] = "False";
            this.SearchProperties[HtmlDocument.PropertyNames.FrameDocument] = "False";
            this.FilterProperties[HtmlDocument.PropertyNames.Title] = "Certify";
            this.FilterProperties[HtmlDocument.PropertyNames.AbsolutePath] = "/SunGardStateInterfaceTest/Certify";
            this.FilterProperties[HtmlDocument.PropertyNames.PageUrl] = "http://psjdevtest/SunGardStateInterfaceTest/Certify";
            this.WindowTitles.Add("Certify");
            #endregion
        }
        
        #region Properties
        public HtmlHyperlink UIUpdateCertificationHyperlink
        {
            get
            {
                if ((this.mUIUpdateCertificationHyperlink == null))
                {
                    this.mUIUpdateCertificationHyperlink = new HtmlHyperlink(this);
                    #region Search Criteria
                    this.mUIUpdateCertificationHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Id] = null;
                    this.mUIUpdateCertificationHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Name] = null;
                    this.mUIUpdateCertificationHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Target] = null;
                    this.mUIUpdateCertificationHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.InnerText] = null;
                    this.mUIUpdateCertificationHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.AbsolutePath] = "/SunGardStateInterfaceTest/Certify/Update";
                    this.mUIUpdateCertificationHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Title] = "Update Certification";
                    this.mUIUpdateCertificationHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Href] = "http://psjdevtest/SunGardStateInterfaceTest/Certify/Update";
                    this.mUIUpdateCertificationHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Class] = "dashboardIcon glyphicon glyphicon-flag glyphicon-white";
                    this.mUIUpdateCertificationHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.ControlDefinition] = "title=\"Update Certification\" class=\"dash";
                    this.mUIUpdateCertificationHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.TagInstance] = "6";
                    this.mUIUpdateCertificationHyperlink.WindowTitles.Add("Certify");
                    #endregion
                }
                return this.mUIUpdateCertificationHyperlink;
            }
        }
        #endregion
        
        #region Fields
        private HtmlHyperlink mUIUpdateCertificationHyperlink;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public class UICertificationUpdateDocument : HtmlDocument
    {
        
        public UICertificationUpdateDocument(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[HtmlDocument.PropertyNames.Id] = null;
            this.SearchProperties[HtmlDocument.PropertyNames.RedirectingPage] = "False";
            this.SearchProperties[HtmlDocument.PropertyNames.FrameDocument] = "False";
            this.FilterProperties[HtmlDocument.PropertyNames.Title] = "Certification Update";
            this.FilterProperties[HtmlDocument.PropertyNames.AbsolutePath] = "/SunGardStateInterfaceTest/Certify/Update";
            this.FilterProperties[HtmlDocument.PropertyNames.PageUrl] = "http://psjdevtest/SunGardStateInterfaceTest/Certify/Update";
            this.WindowTitles.Add("Certification Update");
            #endregion
        }
        
        #region Properties
        public HtmlHyperlink UIGetFormsHyperlink
        {
            get
            {
                if ((this.mUIGetFormsHyperlink == null))
                {
                    this.mUIGetFormsHyperlink = new HtmlHyperlink(this);
                    #region Search Criteria
                    this.mUIGetFormsHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Id] = null;
                    this.mUIGetFormsHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Name] = null;
                    this.mUIGetFormsHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Target] = null;
                    this.mUIGetFormsHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.InnerText] = null;
                    this.mUIGetFormsHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.AbsolutePath] = "/SunGardStateInterfaceTest/Certify/Update";
                    this.mUIGetFormsHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Title] = "Get Forms";
                    this.mUIGetFormsHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Href] = "http://psjdevtest/SunGardStateInterfaceTest/Certify/Update#";
                    this.mUIGetFormsHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Class] = "glyphicon glyphicon-play-circle padLeft";
                    this.mUIGetFormsHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.ControlDefinition] = "title=\"Get Forms\" class=\"glyphicon glyph";
                    this.mUIGetFormsHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.TagInstance] = "7";
                    this.mUIGetFormsHyperlink.WindowTitles.Add("Certification Update");
                    #endregion
                }
                return this.mUIGetFormsHyperlink;
            }
        }
        
        public UIAccordian_FREEPane UIAccordian_FREEPane
        {
            get
            {
                if ((this.mUIAccordian_FREEPane == null))
                {
                    this.mUIAccordian_FREEPane = new UIAccordian_FREEPane(this);
                }
                return this.mUIAccordian_FREEPane;
            }
        }
        #endregion
        
        #region Fields
        private HtmlHyperlink mUIGetFormsHyperlink;
        
        private UIAccordian_FREEPane mUIAccordian_FREEPane;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.30501.0")]
    public class UIAccordian_FREEPane : HtmlDiv
    {
        
        public UIAccordian_FREEPane(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[HtmlDiv.PropertyNames.Id] = "accordian_FREE";
            this.SearchProperties[HtmlDiv.PropertyNames.Name] = null;
            this.FilterProperties[HtmlDiv.PropertyNames.InnerText] = "FREEFree Form Inquiry     \r\n\r\n\r\nUnit Tes";
            this.FilterProperties[HtmlDiv.PropertyNames.Title] = null;
            this.FilterProperties[HtmlDiv.PropertyNames.Class] = "panel-group";
            this.FilterProperties[HtmlDiv.PropertyNames.ControlDefinition] = "class=\"panel-group\" id=\"accordian_FREE\" data-bind=\"attr:{ id: \'accordian_\' + Form" +
                "Id()}\"";
            this.FilterProperties[HtmlDiv.PropertyNames.TagInstance] = "23";
            this.WindowTitles.Add("Certification Update");
            #endregion
        }
        
        #region Properties
        public HtmlHyperlink UIItemHyperlink
        {
            get
            {
                if ((this.mUIItemHyperlink == null))
                {
                    this.mUIItemHyperlink = new HtmlHyperlink(this);
                    #region Search Criteria
                    this.mUIItemHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Id] = null;
                    this.mUIItemHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Name] = null;
                    this.mUIItemHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.Target] = null;
                    this.mUIItemHyperlink.SearchProperties[HtmlHyperlink.PropertyNames.InnerText] = " ";
                    this.mUIItemHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.AbsolutePath] = null;
                    this.mUIItemHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Title] = null;
                    this.mUIItemHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Href] = "#collapse_FREE";
                    this.mUIItemHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.Class] = "glyphicon glyphicon-collapse-down glyphFont";
                    this.mUIItemHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.ControlDefinition] = "class=\"glyphicon glyphicon-collapse-down";
                    this.mUIItemHyperlink.FilterProperties[HtmlHyperlink.PropertyNames.TagInstance] = "1";
                    this.mUIItemHyperlink.WindowTitles.Add("Certification Update");
                    #endregion
                }
                return this.mUIItemHyperlink;
            }
        }
        #endregion
        
        #region Fields
        private HtmlHyperlink mUIItemHyperlink;
        #endregion
    }
}
