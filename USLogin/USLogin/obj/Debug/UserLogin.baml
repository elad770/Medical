   M S B A M L   `   `   `  ����B  >USLogin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:USLoginUSLogin  5          USLogin.UserLogin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:USLogin     5        � 	Resources  5	      *  $USLogin.BooleanToVisibilityConverter    BooleanToVisibilityConverter*  $USLogin.ContentToVisibilityConverter   ContentToVisibilityConverter��%:   &	        &	        5
   
   6-     5   
   6-   5      .��5      � -   5        $
��grid��5      $��Center=�6   $��0,50,0,0q�64    � RowDefinitions 5   
   �� 5      $	��110 �6   �� 5      $��40 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $
��auto �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5!      $
��auto �6   �� 5#      $��30 �6   �� 5%      $��30 �6   �� 5'      $
��auto �6   �� 5)      $��8 �6   �� 5*      $��41 �6   �� 5+       � ColumnDefinitions 5-   
   �� 5.      $
��auto �6   .+�52   
   �� -   5        $��
titleLogin��52      $��1��6&   $��Login��63   ���	#FF7A7171$��23(�6W   $
��Bold"�6e   $��Center=�6x   �� 54   
   -   5        $��	lUserName��54      $
��Bold"�6%   $��1��67   $��2��6G   $��	User Name��6T   ���	#FF7A7171$��Center=�6|   $��Bottom=�6�   �� 55   
   -   5         ��Name$ txtUsername��55      $��30��6)   $��3��6�     textboxPasswordboxStyles#���  6�   $	��290��6�   ��65   �� *
UserName+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ OneWayToSource=�6�   �� 57   
   -   5        $��massageErrorUserName��57      $��4��58   ]   $��1��6k   $��14(�6|   
���  ��y�57   ,   ��  �� RelativeSource �� *Self+ �� Path$ Content�$ PropertyChanged=� �� 	Converter# [ ��58   �   �� *Massages[0]+$ PropertyChanged=��� 59   
   $��Password��6   $��5��6%   $��Center=�62   $��Bottom=�6O   $
��Bold"�6j   ���	#FF7A71716}   G� 5:   
   -   5        	 G�Name$	 txtPassword��5:      #���  6Z   $��1��6�   $��6��6  ��6�   �� 
 �� ElementName$
 txtUsername��$ Height���6�   �� $
 txtUsername��$ Width�6  �� 5;   
   -   5        $��massageErrorPassword��5;      $��7��5<   ]   
���  ��$��1��6}   $��14(�6�   y�5;   ,   ��  �� *Self+$ Content�$ PropertyChanged=�# [ ��5<   �   �� *Massages[1]+$ PropertyChanged=��� 5=   
   $��ID��6   $
��Bold"�6   $��8��61   $��Center=�6>   $��Bottom=�6[   ���	#FF7A71716w   �� 5>   
   -   5        $ txtId��5>      #���  6�   $��21(�6�   $��9��6"  ��6!   �� *Id+$ PropertyChanged=�$ OneWayToSource=���6p   �� $
 txtUsername��$ Height���6�   �� $
 txtUsername��$ Width�6"  �� 5?   
   -	   5        $��massageErrorId��5?      $��10��6&   $��1��64   $��14(�6D   
���  ��y�6c   ��  �� *Self+$ Content�$ PropertyChanged=�# [ ��5@   ]   �� *Massages[2]+$ PropertyChanged=��� 5A   
   -
   5        $��massageError��5A      $ ��The user is not registered��5B   ]   $��10��6�   $��1��6�   $��14(�6�   
���  ��y�5A   '   �� *	IsVisable+$ PropertyChanged=�# [  5B   �   �� 5D   
   -   5         ��Name$	 btn��5D      $��Login��6   $��18(�6.   $��12��6=     btn-primary#��[ 6M   6s   5E      5F      