﻿using System;
using System.Windows;
using System.Windows.Input;

namespace Arduino_Controller
{

    /// <summary>
    /// Vlastní View model pro Material Design okno
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// Okno, které tento ViewModel ovládá
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// Vnější odsazení okna pro efekt stínu
        /// </summary>
        private int mOuterMarginSize = 10;

        /// <summary>
        /// Zoblení okrajů okna
        /// </summary>
        private int mWindowRadius = 10;

        private bool menuOpened;

        #endregion

        #region Public Properties

        /// <summary>
        /// Velikost ohraničení umožňující měnit velikost okna
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// Tloušťka okraje pro manipulaci velikosti okna, která poočíta s vnějším odsazením
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        /// <summary>
        /// Vnejší odsazení okna pro shadow effect
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }

        }

        /// <summary>
        /// Vnejší odsazení okna pro shadow effect
        /// </summary>
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }


        /// <summary>
        /// Zoblení okrajů okna
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }

        }

        /// <summary>
        /// Zoblení okrajů okna
        /// </summary>
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        /// <summary>
        /// Výška title baru ... Titulek okna
        /// </summary>
        public int TitleHeight { get; set; } = 56;

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        #endregion

        #region Commands
        /// <summary>
        /// Příkaz pro minimalizování okna
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// Příkaz pro zavření okna
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// Příkaz pro maximalizování okna
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// Příkaz pro otevření a zavření hlavního menu 
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            mWindow = window;
            mWindow.StateChanged += (sender, e) => {
                // Zkotroluje a spustí eventy pokud se stav okna změnil
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            // Opravuje bug při zvětšovaní okna 
            var WinRes = new WindowResizer(mWindow);

            // Inicilaizace příkazů
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => OpenMenu());
        }

        private void OpenMenu()
        {
            menuOpened ^= true;
            Console.WriteLine($"Menu otevreno: {menuOpened}");
        }
        #endregion
    }
}
