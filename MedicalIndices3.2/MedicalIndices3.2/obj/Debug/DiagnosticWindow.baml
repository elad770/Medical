   M S B A M L   `   `   `  ����L  HMedicalIndices3.2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MedicalIndices3MedicalIndices3  5   	   )    #MedicalIndices3._2.DiagnosticWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MedicalIndices3     5   	     � Title$  Patient Dignostic��5       � WindowState$ 	Maximized=�6"      backgroundImage#���   6:    � WindowStyle$ SingleBorderWindow=�6i    � WindowStartupLocation$ CenterScreen=�6�    � 	Resources 5	       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $! /Resources/AllResource.xaml?�6%   %    5      .��5      �� .��5   
   � -   5        $��GridRoot��5       � RowDefinitions 5      �� 5      $��50 �6    �� 5      $	��210 �6    �� 5      $
��auto �6    �� 5      $��10 �6    .+�5      � $��0��6   5!      � 5"      $��1��6    � ColumnDefinitions 5#      �� 5$      $��* �6'   �� 5%      $	��350 �6'   .+�5(      �� -   5        $��border��5(        BorderStyle#��[ 6'   $��1��6L   $��0q�5)      $��
0,10,20,10q�61   ._�5*      �  5+      �� 5,      $��10 �6,   �� 5-      $��40 �6,   �� 5.      $��40 �6,   �� 5/      $��40 �6,   �� 50      $��40 �6,   �� 51      $��40 �6,   �� 52      $��10 �6,   .+�56      �� -   5        $��laName��56       $��שם:��6.     resTextLabel#��[ 6<   $��1��6b   $��0,0,20,0q�6o   $��Right=�6�   $��RightToLeft=�6�   �� 57      -   5        $��tbName��57   "   {�.  $��1��6]   $��0,0,80,0q�6j   $��Right=�6|   $��RightToLeft=�6�   $��Center=�6�     
resTextBox#��[ 6�   
���������60   �� 	 �� Path$
	 Name�6�   �� 59      $��:ת.ז��6!   #��[ 60   $��2��6�   $��Right=�6�   ��6V   �� 
 �� ElementName$
 laName��$	 Margin�6�   �� 5:      -   5        $
��tbId��5:   "   {�.  $��2��6Y   $��Right=�6�   $��RightToLeft=�6�   $��Center=�6�   #��[ 6�   ��6.   �� $	 Id���6f   �� $	 Margin�$
 tbName����6  �� $
 tbName��$	 
Background��� 5;      $��:גיל��6!   #��[ 60   $��3��6V   $��Right=�6e   ��6�   �� $
 laName��$	 Margin��� 5<      -   5        $��tbAge��5<   "   {�.  $��3��6[   $��Right=�6�   $��RightToLeft=�6�   $��Center=�6�   #��[ 6�   ��6/   �� $		 Age���6h   �� $	 Margin�$
 tbName����6  �� $
 tbName��$	 
Background��� 5=      $��:מין��6!   #��[ 60   $��4��6�   $��Right=�6�   ��6V   �� $	 Margin�$
 laName��6�   �� 5?      -   5        $��tbGen��5?   "   {�.  $��4��6B   $��Right=�6�   $��RightToLeft=�6�   $��Center=�6�   #��[ 6�   
���������6O   �� $
 tbName��$	 Margin�6�   5A      5B      5C      �� 5D      -   5        $��totalResult��5D      $��	Underline{�6,   $��2��6�   $��Center=�6  $
��Bold"�6"  ��6R   �� $
 laName��$	 FontSize���6�   �� $
 laName��$	 
Foreground���6�   �� $
 laName��$	 Width�6"  5E      5F      5G      