   M S B A M L   `   `   `  ����J  Fself-adaptation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:self_adaptationself_adaptation  5   	        self_adaptation.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:self_adaptation     5   	     � Title$  
MainWindow��5   	   $	��699��5	   	   $
��1200��6   $	��500��6#    � WindowStyle$
 None=�5
   	   
������  � AllowsTransparency .  � 
ResizeMode$ CanResizeWithGrip=�5   	    � WindowStartupLocation$ Manual=�6(    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ ResponsiveDictionary.xaml?�6%   %    5      .��5      � .+�5   
   �� ���#292D34���#3A4149$��2q�5       �� CornerRadius$ 9,9,0,9��5      �� 5   
   ��6   �� ��5      �� 5      	 �� 
TargetType"	 ��6   .�5      �� 
 �� Property
 � ��   
MainLayout �� Value# [  68   6^    �� Triggers 5      �� 5       �� Binding 6&   �� *ActualWidth+ �� RelativeSource ��  �� AncestorType$ WindowL�  GreaterThanConverter �� 	Converter# [  �� ConverterParameter$
 1250�� �� Value true5   &   .a�5       �� 
 � ��  Greater1250Layout# [ 6@   5!      �� 5"       6&   �� *ActualWidth+ �� $ WindowL�  LessThanConverter# [ $
 1000�� true5#   &   .a�5$      �� 
 � ��  Less1000Layout# [ 6@   5%      5'      5)      5*      5+      