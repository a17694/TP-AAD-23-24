﻿using System.Windows;
using System.Windows.Controls;
using InterfaceAAD.Views;

namespace InterfaceAAD
{
    /// <summary>
    /// Represents the main window of the application.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Default Initialization Page
            MenuList.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the selection changed event of the menu list.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Navigation
            switch (((ListBoxItem)MenuList.SelectedItem)?.Content?.ToString())
            {
                case "Clientes":
                    ContentFrame.Navigate(new ClientsListView());
                    break;

                case "Outra Página":
                    ContentFrame.Navigate(new OutraPagina());
                    break;
            }
        }
    }
}