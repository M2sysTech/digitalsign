//-----------------------------------------------------------------------
// <copyright file="UseWaitcursor.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.UI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Classe para manipular ponteiro do mouse
    /// </summary>
    public class UseWaitCursor : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the UseWaitCursor class
        /// </summary>
        public UseWaitCursor()
        {
            Enabled = true;
        }

        /// <summary>
        ///  Gets or sets a value indicating whether
        /// </summary>
        public static bool Enabled
        {
            get 
            { 
                return Application.UseWaitCursor; 
            }

            set
            {
                if (value == Application.UseWaitCursor)
                {
                    return;
                }

                Application.UseWaitCursor = value;
                Form f = Form.ActiveForm;
                if (f != null && f.Handle != null)
                {
                    // Send WM_SETCURSOR
                    SendMessage(f.Handle, 0x20, f.Handle, (IntPtr)1);
                }
            }
        }

        /// <summary>
        /// Mostra ponteiro normal quando a classe é descarregada
        /// </summary>
        public void Dispose()
        {
            Enabled = false;
        }

        /// <summary>
        /// API do Windows
        /// </summary>
        /// <param name="hwnd">Parâmetro IntPtr hwnd</param>
        /// <param name="msg">Parâmetro int msg</param>
        /// <param name="wp">Parâmetro IntPtr wp</param>
        /// <param name="lp">Parâmetro IntPtr lp</param>
        /// <returns>retorna um inteiro. 1 = enviado e 0 = não enviado</returns>
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp);
    }
}