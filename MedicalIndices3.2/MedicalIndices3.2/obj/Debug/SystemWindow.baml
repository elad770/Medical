   M S B A M L   `   `   `  ����L  HMedicalIndices3.2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MedicalIndices3._2MedicalIndices3._2  5   	   %    MedicalIndices3._2.SystemWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:MedicalIndices3._2     5   	   -   5          � Title$  System Window��5   	    � WindowState$ 	Maximized=�6      backgroundImage2#���   6P   $	��800��6�   $
��1400��6�    � WindowStyle$ SingleBorderWindow=�6�    � WindowStartupLocation$ CenterScreen=�6�    � 	Resources 5	       �� MergedDictionaries �� Source �� 
TargetType �� BasedOn	 �� Property
 �� Value  BorderStyleMore  BorderStyle2 �� Triggers P� Property P� Value�� 5
   
    5      �� 5      $! /Resources/AllResource.xaml?�6%   �� 5      $N Hpack://application:,,,/BootstrapWpfStyle;component/Styles/Bootstrap.xaml?�6%   %$  '	�       '�� 7     '�� n     &	 �     7  �� 5      " 	�5       5      Z� 5      �� 5      " ��5       5      Z� 5      �� 5      " ��6   .�5      �� 	 � ��$	
 160��6+   �� 5      	 � ��$	
 140��6*   5      �� 5      " ��6,   8   .�5      �� 	 � ��$	
 150��6+   �� 5      	 � ��$	
 150��6*   -   5        �� 5      	 �@�Cursor$

 Hand��6+    5      P� 5       �@J�IsMouseOver . . �5       �� 	 � ��
 �	#FF87CEEB67   5!      5#      5$      .��5(      � $��Center=�6   .+�5*   
   �� .+�5+      �� $��-250,40,0,0q�6     BorderStyle#��[ 6*   $
��auto��6P   $	��290��6^   ._�5,      �� -   5        $��	userTitle��5,      $��Center=�6*   $��0,0,0,0q�6G   $��27(�6X   $��Calibri Light)�6f     resTextLabel#��[ 6�   $
��auto��6�   $
��auto��6�   5-      �� 5.      $��Select Task��6   $��-250,160,0,0q�6+   $��Center=�6A   #��[ 6^   $��38(�6�   $��Calibri Light)�6�   50      �� 51   
   $��Center=�6   $��
-250,0,0,0q�62    �� Orientation$ 
Horizontal=�6F   .+�53      �� -   5        $��border1��53      #��[ 6$   ._�54      �� .+�55      �� $��Images\test-indices.png��6   $	��150��6=   $	��125��6J   56      57      �� 58      -   5        $��border2��58   (   $��50,0,0,0q�6   #��[ 67   ._�59      �� .+�5:      �� $��Images\prognosis.png��6   $	��150��6:   $	��125��6G   5<      5=      �� 5@      -   5        $��
borderEdit��5@      $��50,0,0,0q�6(   #��[ 6:   ._�5A      �� .+�5B      �� $��Images\help.png��6   $	��150��65   $	��125��6B   5D      5E      5F      �� 5G   
   $��New Test��6   $��-210,475,0,0q�6$   #��[ 6;   $��17(�6a   �� 5H   
   $��	All Tests��6   $��
-5,475,0,0q�6$   $��17(�68   #��[ 6G   �� 5I   
   $��Edit Doctor Details��6   $��160,475,0,0q�6/   $��17(�6D   #��[ 6T   5L      5M      