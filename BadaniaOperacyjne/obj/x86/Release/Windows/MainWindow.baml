   M S B A M L   `   `   `  ���� L  HBadaniaOperacyjne, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BadaniaOperacyjne.WindowsBadaniaOperacyjne.Windows  *    $BadaniaOperacyjne.Windows.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ;windows'clr-namespace:BadaniaOperacyjne.Windows       � Title$  Problem komiwojażera��$	��525�� � 
ResizeMode$ CanMinimize=� � SizeToContent$ Height=� J� CommandBindings )  #System.Windows.Input.CommandBinding  -   *   $BadaniaOperacyjne.Windows.MainWindow   OpenSolutionCommand   Command# Z .��� $��10q�.+��  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $��* ��� $��* �.+��� $��0��$��0��$x��rOtwórz menedżera zagadnień, by rozwiązać nowy problem komiwojażera lub wczytać wcześniej utworzony problem��$
��Wrap=�$��5q�$��Center=��� -   $��0��$��1��$��Rozwiąż problem��$��25��$��5q��� $��1��$��0��$"��Graf do rysowania problemów��$
��Wrap=�$��5q�$��Center=��� -   $��1��$��1��$��Otwórz��$��25��$��5q��� $��2��$��0��$9��3Wybierz spośród zapisanych rozwiązań problemów��$
��Wrap=�$��5q�$��Center=��� $��2��$��1��$��Wczytaj rozwiązanie��$��25��$��5q�#��Z �� $��3��$��0��$9��3Otwórz ustawienia, by zmienić parametry algorytmu��$
��Wrap=�$��5q�$��Center=��� -   $��3��$��1��$��
Ustawienia��$��25��$��5q�