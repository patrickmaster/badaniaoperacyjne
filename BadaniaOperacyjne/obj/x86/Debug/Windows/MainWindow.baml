   M S B A M L   `   `   `  ����L  HBadaniaOperacyjne, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:BadaniaOperacyjne.WindowsBadaniaOperacyjne.Windows  5   	   *    $BadaniaOperacyjne.Windows.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ;windows'clr-namespace:BadaniaOperacyjne.Windows     5   	     � Title$  Problem komiwojażera��5   	   $	��525��6&    � 
ResizeMode$ CanMinimize=�62    � SizeToContent$ Height=�5   	    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5        *   $BadaniaOperacyjne.Windows.MainWindow   OpenSolutionCommand   Command# Z 5	      5
      .��5   
   � $��10q�6   .+�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��0��6%   $x��rOtwórz menedżera zagadnień, by rozwiązać nowy problem komiwojażera lub wczytać wcześniej utworzony problem��65   $
��Wrap=�6�   $��5q�6�   $��Center=�6�   6�   �� 5      -   5        $��0��5      $��1��6"   $��Rozwiąż problem��62   $��25��6L   $��5q�6X   6�   �� 5      $��2��6   $��0��6%   $9��3Wybierz spośród zapisanych rozwiązań problemów��65   $
��Wrap=�6k   $��5q�6   $��Center=�6�   6�   �� 5      $��2��6   $��1��6"   $��Wczytaj rozwiązanie��62   $��25��6P   $��5q�6\   #��Z 6g   6�   �� 5!      $��3��6   $��0��6%   $9��3Otwórz ustawienia, by zmienić parametry algorytmu��65   $
��Wrap=�6n   $��5q�6�   $��Center=�6�   6�   �� 5"      -   5        $��3��5"      $��1��6"   $��
Ustawienia��62   $��25��6G   $��5q�6S   6z   5$      5%      5&      