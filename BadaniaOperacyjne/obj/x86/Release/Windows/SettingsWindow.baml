   M S B A M L   `   `   `  ���� L  HBadaniaOperacyjne, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:BadaniaOperacyjne.ValidationRules!BadaniaOperacyjne.ValidationRules  .    (BadaniaOperacyjne.Windows.SettingsWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     F
validation/clr-namespace:BadaniaOperacyjne.ValidationRules       � Title$  Ustawienia algorytmu��$	��400�� � SizeToContent$ Height=� � 
ResizeMode$ CanMinimize=�.���  � RowDefinitions �� $
��auto ��� $
��auto �.+�� $��0�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � � ColumnDefinitions �� $
��1.2* ��� $��* �.+��� $��0��$��0��$��Temperatura początkowa��$��5q�$��Center=��� $��0��$��1��$��5q�$��2q�.������  �� Path$ StartingTemperature� �� ValidationRules 1   +BadaniaOperacyjne.ValidationRules.RangeRule     Minimum$ 0Q�   Maximum$	 100Q�.���� $��1��$��0��$��Temperatura końcowa��$��5q�$��Center=��� $��1��$��1��$��5q�$��2q�.������ $ EndingTemperature�   $ 0Q�$	 100Q�.���� $��2��$��0��$ ��Współczynnik chłodzenia��$��5q�$��Center=��� $��2��$��1��$��5q�$��2q�.������ $ CoolingCoefficient�   $ 0Q�$ 1Q�	   	Exclusive	 . .���� $��3��$��0��$'��!Liczba iteracji w pierwszym bloku��$��5q�$��Center=��� $��3��$��1��$��5q�$��2q�.������ $ NumIterations�   $	 100Q�$ 10000Q�.���� $��4��$��0��$��Mnożnik liczby iteracji��$��5q�$��Center=��� $��4��$��1��$��5q�$��2q�.������ $ NumIterationsMultiplier�   $ 1Q�$ 10Q�.���� $��5��$��0��$��Typy sąsiedztwa��$��5q�$��Center=��� $��5��$��1��$��5q�$��2q�
 �� SelectedValuePath$
 Value�� �� DisplayMemberPath$	 Key������ *OperationsList+ �� SelectedValue �� *	Operation+� $��1��$	��300��$��Right=� �� $��* ��� $��* �.+��� -   $��0��$��Zapisz��$��25��$��5q��� -   $��1��$��Anuluj��$��25��$��5q�