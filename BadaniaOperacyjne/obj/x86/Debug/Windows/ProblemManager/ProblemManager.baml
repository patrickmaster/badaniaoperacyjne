   M S B A M L   `   `   `  ����L  HDataGrid2DLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:DataGrid2DLibrary;assembly=DataGrid2DLibraryDataGrid2DLibrary  5   	   L HBadaniaOperacyjne, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:BadaniaOperacyjne.ValidationRules!BadaniaOperacyjne.ValidationRules 5	   	   =   7BadaniaOperacyjne.Windows.ProblemManager.ProblemManager   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Kdg2d:clr-namespace:DataGrid2DLibrary;assembly=DataGrid2DLibrary     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   F
validation/clr-namespace:BadaniaOperacyjne.ValidationRules     5	   	   -   5        $��ProblemManagerWindow��5   	     � 
ResizeMode$  	CanResize=�5   4    � SizeToContent$ WidthAndHeight=�5
   	   $	��350��5   	   $	��250��6    � Title 5   	   �� *WindowTitle+ �� Mode$ OneWay=� J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ ApplicationCommands.New��5      5        5   
   -   5        $ ApplicationCommands.Open��5      5        5   
   -   5        $ ApplicationCommands.Save��5      5        5   
   -   5        $  ApplicationCommands.SaveAs��5      5        5   
   -   5        $ ApplicationCommands.Close��5      5      .��5      �  � RowDefinitions 5   
   �� 5       $
��auto �6   �� 5!      $��* �6   �� 5"      $
��auto �6   .+�5$   
   o� $��0��6   $��0q�6   .@�5%      m� $
��Plik��6   .@�5&      m�  m� Command$ ApplicationCommands.New��6   $
��Nowy��6=   m� 5'      $ ApplicationCommands.Open��6   $��Otwórz��6>   m� 5(      $ ApplicationCommands.Save��6   $��Zapisz��6>   m� 5)      $  ApplicationCommands.SaveAs��6   $��Zapisz jako��6@   m� 5*      $ ApplicationCommands.Close��6   $��Wyjdź��6?   5+      5,      � 5-   
   $��1��6    5.      �� 5/      $
��Auto �6    �� 50      $��* �6    .+�52      � $��0��6    � ColumnDefinitions 53      �� 54      $
��auto �6'   �� 55      $
��auto �6'   .+�57      "   DataGrid2DLibrary.DataGrid2D  -   5        $��dataGrid��58   *   $��0��57   "   $	��Top=�58      $��0q�59   X   &   System.Windows.Controls.DataGrid	   CanUserReorderColumns	 .  
   CanUserResizeColumns
 .     CanUserResizeRows .     ItemsSource2D 6   �� *	ItemsList+ �� UpdateSourceTrigger$ 	LostFocus=�   ColumnHeaderStyle 5;      �� 5<       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5=      -   5        5>         RowHeaderStyle 5@      �� 5A      :  4System.Windows.Controls.Primitives.DataGridRowHeader"  6    .�5B      -	   5        5C      5E      � 5F      $��1��6   $	��220��6'    5G      �� 5H      $
��Auto �6(    5J      �� 5K      $��* �6+   �� 5L      $��* �6+   .+�5N      �� $��0��6    $��0��6-   $��Pojemność baku��6=   $��5q�6S   $��Center=�6^   �� 5O      $��0��6   $��1��6+   $��5q�6;   $��2q�6F   .��6R   ��5P      �� 5Q       �� Path$ FuelCapacity�6&    �� ValidationRules 5R   "   1  +BadaniaOperacyjne.ValidationRules.RangeRule  5S   &      Minimum$ 0Q�6;      Maximum$
 5000Q�6G   5U      .��5V   (   5W      5X      5Y      � 5Z      $��1��6   $��Right=�5[      $��Bottom=�6-    5\      �� 5]      $	��130 �6'   �� 5^      $	��130 �6'   .+�5`      �� -
   5        $��0��5`      $��	Rozwiąż��6)   $��25��6;   $��5q�6G   6R   �� 5a      $��1��6   $��Anuluj��6)   $��25��6:   $��5q�6F   $��ApplicationCommands.Close��6Q   5b      5c      5d      5e      