/**
 * Copyright (c) 2019 Emilian Roman
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 */

using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.DialogResult;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Placien
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  /// 



  public partial class MainWindow
  {


        static string DestinationFolder = "";
        static string HostFolder = "";
        static string TagClass = "";
        static string TagFolder = "";
        static int TagLength = 0;

        private readonly Main _main;

    public MainWindow()
    {
      InitializeComponent();
      

      _main = (Main) DataContext;
      _main.Load();

     
    }

    private void BrowsePlaceholder(object sender, RoutedEventArgs e)
    {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == OK)
                    _main.Placeholder = dialog.SelectedPath;
            }
        }

    private void GetTags(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == OK)
                    TagFolder = dialog.SelectedPath;
                tagbox.Text = TagFolder;
                
            }
        }

        private void BrowseTarget(object sender, RoutedEventArgs e)
    {
      using (var dialog = new FolderBrowserDialog())
      {
        if (dialog.ShowDialog() == OK)
          _main.Directory = dialog.SelectedPath;
      }
    }

    private void BrowseSapien(object sender, RoutedEventArgs e)
    {
      var dialog = new OpenFileDialog
      {
        Filter = "Sapien executable (*.exe)|*.exe"
      };

      if (dialog.ShowDialog() == true)
        _main.Sapien = dialog.FileName;

      
    }

 

    

    private void Binaries(object sender, RoutedEventArgs e)
    {
      Process.Start("https://dist.n2.network/placien/");
    }

    private void Source(object sender, RoutedEventArgs e)
    {
      Process.Start("https://cgit.n2.network/placien/");
    }

    private async void ApplyPlaceholder(object sender, RoutedEventArgs e)
    {
      ApplyPlaceholderButton.IsEnabled = false;
      ApplyPlaceholderButton.Content   = "Applying...";
            HostFolder = hostbox.Text;
            DestinationFolder = targetbox.Text;
            TagClass = typebox.Text;

            TagFolder = tagbox.Text;
            TagLength = TagFolder.Length;

            await Task.Run(() => { _main.Copy(HostFolder, DestinationFolder, TagClass, TagLength); });

      ApplyPlaceholderButton.Content = "Success!";

      await Task.Run(() => { Thread.Sleep(3000); });

      ApplyPlaceholderButton.IsEnabled = true;
      ApplyPlaceholderButton.Content   = "Apply placeholder";
    }

        private void Tagbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}