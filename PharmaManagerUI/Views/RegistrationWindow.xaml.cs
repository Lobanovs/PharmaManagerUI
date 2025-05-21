﻿using PharmaManagerUI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PharmaManagerUI.Views
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel(); // Устанавливаем DataContext здесь
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.Password = passwordBox.Password;
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel && sender is PasswordBox confirmPasswordBox)
            {
                viewModel.ConfirmPassword = confirmPasswordBox.Password;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}