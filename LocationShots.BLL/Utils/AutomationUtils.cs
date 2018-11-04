using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace LocationShots.BLL
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ms750582%28v=vs.110%29.aspx
    /// </summary>
    class AutomationUtils
    {
        /// <summary>
        /// <see cref="http://pinvoke.net/default.aspx/Enums/ShowWindowCommand.html"/>
        /// </summary>
        public enum ShowWindowCommands
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            Hide = 0,
            /// <summary>
            /// Activates and displays a window. If the window is minimized or
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when displaying the window
            /// for the first time.
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// Maximizes the specified window.
            /// </summary>
            Maximize = 3, // is this the right value?
                          /// <summary>
                          /// Activates the window and displays it as a maximized window.
                          /// </summary>      
            ShowMaximized = 3,
            /// <summary>
            /// Displays a window in its most recent size and position. This value
            /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except
            /// the window is not activated.
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// Activates the window and displays it in its current size and position.
            /// </summary>
            Show = 5,
            /// <summary>
            /// Minimizes the specified window and activates the next top-level
            /// window in the Z order.
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// Displays the window as a minimized window. This value is similar to
            /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the
            /// window is not activated.
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// Displays the window in its current size and position. This value is
            /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the
            /// window is not activated.
            /// </summary>
            ShowNA = 8,
            /// <summary>
            /// Activates and displays the window. If the window is minimized or
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when restoring a minimized window.
            /// </summary>
            Restore = 9,
            /// <summary>
            /// Sets the show state based on the SW_* value specified in the
            /// STARTUPINFO structure passed to the CreateProcess function by the
            /// program that started the application.
            /// </summary>
            ShowDefault = 10,
            /// <summary>
            ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
            /// that owns the window is not responding. This flag should only be
            /// used when minimizing windows from a different thread.
            /// </summary>
            ForceMinimize = 11
        }

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern int FindWindowEx(int hwndParent, int hwndEnfant, int lpClasse, string lpTitre);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);


        #region API
        //
        public static void SetWindowState(string title, ShowWindowCommands cmd)
        {
            IntPtr hwnd = FindWindowByCaption(IntPtr.Zero, title);
            ShowWindow(hwnd, cmd);
        }

        public static bool HandleLogin(string user, string pass)
        {
            try
            {
                int dialogHanlde = GetWindowHandle("Windows Security");
                AutomationElement dialog = AutomationElement.FromHandle((IntPtr)dialogHanlde);

                var textBoxes = dialog.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit)).Cast<AutomationElement>();
                var buttons = dialog.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)).Cast<AutomationElement>();
                AutomationElement userEdit = textBoxes.FirstOrDefault(t => t.Current.Name.Equals("User name"));
                AutomationElement passEdit = textBoxes.FirstOrDefault(t => t.Current.Name.Equals("Password"));
                AutomationElement okButton = buttons.FirstOrDefault(b => b.Current.Name.Equals("OK"));
                if (okButton == null || userEdit == null || passEdit == null)
                    return false;

                InsertTextUsingUIAutomation(userEdit, user);
                InsertTextUsingUIAutomation(passEdit, pass);
                var invokePattern = okButton.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                invokePattern.Invoke();
                //SendKeys.SendWait("{Enter}");

                return true;
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool HandleSaveAs_OldIE(string filename)
        {
            try
            {
                int dialogHanlde = GetWindowHandle("Save As");
                AutomationElement dialog = AutomationElement.FromHandle((IntPtr)dialogHanlde);
                var textBoxes = dialog.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit)).Cast<AutomationElement>();
                var buttons = dialog.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)).Cast<AutomationElement>();
                AutomationElement theEdit = textBoxes.FirstOrDefault(t => t.Current.Name.Equals("File name:"));
                AutomationElement theButton = buttons.FirstOrDefault(b => b.Current.Name.Equals("Save"));
                if (theButton == null || theEdit == null)
                    return false;

                InsertTextUsingUIAutomation(theEdit, filename);
                var invokePattern = theButton.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                invokePattern.Invoke();

                return true;
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dialogTitle"></param>
        /// <param name="buttonText"></param>
        /// <returns></returns>
        public static bool ClickDialogButton(string dialogTitle, string buttonText)
        {
            try
            {
                int dialogHanlde = GetWindowHandle(dialogTitle);
                AutomationElement dialog = AutomationElement.FromHandle((IntPtr)dialogHanlde);
                var buttons = dialog.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)).Cast<AutomationElement>();
                AutomationElement theButton = buttons.FirstOrDefault(b => b.Current.Name.Equals(buttonText));
                if (theButton == null)
                    return false;

                var invokePattern = theButton.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                invokePattern.Invoke();

                /*
                 * // NO clickable point!
                System.Windows.Point p = theButton.GetClickablePoint();
                AutoItX3Lib.AutoItX3Class au3;
                au3 = new AutoItX3Lib.AutoItX3Class();
                au3.AutoItSetOption("MouseCoordMode", 0);
                au3.MouseClick("LEFT", (int)p.X, (int)p.Y, 1, -1);
                */

                return true;
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                return false;
            }
        }
        /// </summary>
        /// http://stackoverflow.com/questions/562392/getting-the-handle-of-window-in-c-sharp
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static int GetWindowHandle(string title)
        {
            int hwnd = FindWindowEx(0, 0, 0, title);    //where title is the windowtitle

            //verification of the window
            if (hwnd == 0)
            {
                throw new Exception("Window not found");
            }
            return hwnd;
        }
        /// </summary>
        /// <param name="windowTitle"></param>
        public static void CloseWindowForTitle(string windowTitle)
        {
            int windowHandle = GetWindowHandle(windowTitle);
            AutomationElement window = AutomationElement.FromHandle((IntPtr)windowHandle);
            var windowPattern = window.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
            windowPattern.Close();
        }
        /// <summary>
        /// http://stackoverflow.com/questions/10105396/given-an-automation-element-how-do-i-simulate-a-single-left-click-on-it
        /// </summary>
        /// <param name="automationElement"></param>
        public static void InvokeAutomationElement(AutomationElement automationElement)
        {
            var invokePattern = automationElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            invokePattern.Invoke();
        }
        /// <summary>
        /// http://www.codeproject.com/Articles/60872/Automate-Software-using-WPF-UI-Automation
        /// </summary>
        /// <param name="element"></param>
        private void ClickElement(AutomationElement element)
        {
            if (element != null)
            {
                if (element.Current.ControlType.Equals(ControlType.Button))
                {
                    InvokePattern pattern =
                        element.GetCurrentPattern
                        (InvokePattern.Pattern) as InvokePattern;
                    pattern.Invoke();
                    Wait(2);
                }
                else if (element.Current.ControlType.Equals(ControlType.RadioButton))
                {
                    SelectionItemPattern pattern =
                        element.GetCurrentPattern
                        (SelectionItemPattern.Pattern)
                        as SelectionItemPattern;
                    pattern.Select();
                    Wait(2);
                }
            }
        }
        /// <summary>
        /// Obtains an ExpandCollapsePattern control pattern from an automation element.
        /// https://msdn.microsoft.com/en-us/library/system.windows.automation.expandcollapsepattern.collapse%28v=vs.90%29.aspx
        /// </summary>
        /// <param name="targetControl">
        /// The automation element of interest.
        /// </param>
        /// <returns>
        /// A ExpandCollapsePattern object.
        /// </returns>
        private ExpandCollapsePattern GetExpandCollapsePattern(AutomationElement targetControl)
        {
            ExpandCollapsePattern expandCollapsePattern = null;

            try
            {
                expandCollapsePattern =
                    targetControl.GetCurrentPattern(
                    ExpandCollapsePattern.Pattern)
                    as ExpandCollapsePattern;
            }
            // Object doesn't support the ExpandCollapsePattern control pattern.
            catch (InvalidOperationException)
            {
                return null;
            }

            return expandCollapsePattern;
        }
        /// <summary>
        /// Programmatically expand or collapse a menu item.
        /// https://msdn.microsoft.com/en-us/library/system.windows.automation.expandcollapsepattern.collapse%28v=vs.90%29.aspx
        /// </summary>
        /// <param name="menuItem">
        /// The target menu item.
        /// </param>
        ///--------------------------------------------------------------------
        private void ExpandCollapseMenuItem(AutomationElement menuItem)
        {
            if (menuItem == null)
            {
                throw new ArgumentNullException(
                    "AutomationElement argument cannot be null.");
            }

            ExpandCollapsePattern expandCollapsePattern =
                GetExpandCollapsePattern(menuItem);

            if (expandCollapsePattern == null)
            {
                return;
            }

            if (expandCollapsePattern.Current.ExpandCollapseState ==
                ExpandCollapseState.LeafNode)
            {
                return;
            }

            try
            {
                if (expandCollapsePattern.Current.ExpandCollapseState == ExpandCollapseState.Expanded)
                {
                    // Collapse the menu item.
                    expandCollapsePattern.Collapse();
                }
                else if (expandCollapsePattern.Current.ExpandCollapseState == ExpandCollapseState.Collapsed ||
                    expandCollapsePattern.Current.ExpandCollapseState == ExpandCollapseState.PartiallyExpanded)
                {
                    // Expand the menu item.
                    expandCollapsePattern.Expand();
                }
            }
            // Control is not enabled
            catch (ElementNotEnabledException)
            {
                // TO DO: error handling.
            }
            // Control is unable to perform operation.
            catch (InvalidOperationException)
            {
                // TO DO: error handling.
            }
        }
        //
        #endregion API

        #region UTL
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/ms750582.aspx
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        private static bool InsertTextUsingUIAutomation(AutomationElement element, string value)
        {
            StringBuilder feedbackText = new StringBuilder();
            try
            {
                // Validate arguments / initial setup 
                if (value == null)
                    throw new ArgumentNullException(
                        "String parameter must not be null.");

                if (element == null)
                    throw new ArgumentNullException(
                        "AutomationElement parameter must not be null");

                // A series of basic checks prior to attempting an insertion. 
                // 
                // Check #1: Is control enabled? 
                // An alternative to testing for static or read-only controls  
                // is to filter using  
                // PropertyCondition(AutomationElement.IsEnabledProperty, true)  
                // and exclude all read-only text controls from the collection. 
                if (!element.Current.IsEnabled)
                {
                    throw new InvalidOperationException(
                        "The control with an AutomationID of "
                        + element.Current.AutomationId.ToString()
                        + " is not enabled.\n\n");
                }

                // Check #2: Are there styles that prohibit us  
                //           from sending text to this control? 
                if (!element.Current.IsKeyboardFocusable)
                {
                    throw new InvalidOperationException(
                        "The control with an AutomationID of "
                        + element.Current.AutomationId.ToString()
                        + "is read-only.\n\n");
                }


                // Once you have an instance of an AutomationElement,   
                // check if it supports the ValuePattern pattern. 
                object valuePattern = null;

                // Control does not support the ValuePattern pattern  
                // so use keyboard input to insert content. 
                // 
                // NOTE: Elements that support TextPattern  
                //       do not support ValuePattern and TextPattern 
                //       does not support setting the text of  
                //       multi-line edit or document controls. 
                //       For this reason, text input must be simulated 
                //       using one of the following methods. 
                //        
                if (!element.TryGetCurrentPattern(
                    ValuePattern.Pattern, out valuePattern))
                {
                    feedbackText.Append("The control with an AutomationID of ")
                        .Append(element.Current.AutomationId.ToString())
                        .Append(" does not support ValuePattern.")
                        .AppendLine(" Using keyboard input.\n");

                    // Set focus for input functionality and begin.
                    element.SetFocus();

                    // Pause before sending keyboard input.
                    Thread.Sleep(100);

                    // Delete existing content in the control and insert new content.
                    SendKeys.SendWait("^{HOME}");   // Move to start of control
                    SendKeys.SendWait("^+{END}");   // Select everything
                    SendKeys.SendWait("{DEL}");     // Delete selection
                    SendKeys.SendWait(value);
                }
                // Control supports the ValuePattern pattern so we can  
                // use the SetValue method to insert content. 
                else
                {
                    feedbackText.Append("The control with an AutomationID of ")
                        .Append(element.Current.AutomationId.ToString())
                        .Append((" supports ValuePattern."))
                        .AppendLine(" Using ValuePattern.SetValue().\n");

                    // Set focus for input functionality and begin.
                    element.SetFocus();

                    ((ValuePattern)valuePattern).SetValue(value);
                }
            }
            catch (ArgumentNullException exc)
            {
                feedbackText.Append(exc.Message);
            }
            catch (InvalidOperationException exc)
            {
                feedbackText.Append(exc.Message);
            }
            finally
            {
                //Feedback(feedbackText.ToString());
            }

            return String.IsNullOrEmpty(feedbackText.ToString());
        }
        /// <summary>
        /// works better with chrome dialogs
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool InsertTextUsingUIAutomation2(AutomationElement element, string value)
        {
            try
            {
                //element.SetFocus();

                //ValuePattern etb = EditableTextBox.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                //etb.SetValue("test");

                //TextPattern tp = (TextPattern)element.GetCurrentPattern(TextPattern.Pattern);


                ValuePattern valueP = element.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                valueP.SetValue(value);

                return true;
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                return false;
            }
        }
        /// <summary>
        /// http://www.codeproject.com/Articles/60872/Automate-Software-using-WPF-UI-Automation
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="nameValue"></param>
        /// <returns></returns>
        private AutomationElement GetElementByNameProperty(AutomationElement parentElement, string nameValue)
        {
            System.Windows.Automation.Condition condition =
                new PropertyCondition(AutomationElement.NameProperty, nameValue);
            AutomationElement element =
                parentElement.FindFirst(TreeScope.Descendants, condition);
            return element;
        }
        /// <summary>
        /// http://www.codeproject.com/Articles/60872/Automate-Software-using-WPF-UI-Automation
        /// </summary>
        /// <param name="rootElement"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool SetValueInTextBox(AutomationElement rootElement, string value)
        {
            System.Windows.Automation.Condition textPatternAvailable =
                new PropertyCondition
                (AutomationElement.IsTextPatternAvailableProperty, true);
            AutomationElement txtElement =
                rootElement.FindFirst(TreeScope.Descendants, textPatternAvailable);
            if (txtElement != null)
            {
                try
                {
                    Console.WriteLine("Setting value in textbox");
                    ValuePattern valuePattern =
                        txtElement.GetCurrentPattern
                        (ValuePattern.Pattern) as ValuePattern;
                    valuePattern.SetValue(value);
                    Wait(2);
                    return true;
                }
                catch
                {
                    Console.WriteLine("Error");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seconds"></param>
        private void Wait(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }
        #endregion UTL
    }
}
