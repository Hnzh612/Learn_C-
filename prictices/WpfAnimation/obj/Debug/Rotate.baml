   M S B A M L   `   `   `  ����G  CWpfAnimation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WpfAnimationWpfAnimation  5   	       WpfAnimation.Rotate   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:WpfAnimation     5   	     � Title$  Rotate��5   	   $	��450��6   $	��800��6%    � 	Resources 5	         sb g� RepeatBehavior �� 
TargetName �� TargetProperty �� KeyTime �� Value��%�   &	        �� 5   
   .��5      �� $ Forever��6+   $ _border��6D   $ Background.Color�6d   .g�5      p� $ 0:0:0.5��6(   $	 Red��6:   6H   p� 5      $ 0:0:1��6(   $ Orange��68   6I   5      5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ Button.Click��6   	 :� 
SourceName$	 _button��62   .[�5      �� -   5        
 ��Name$	
 bsb��5   ?   #��[  6   6?   5      :� 5   
   $ Button.Click��6   $	 _button1��62   .[�5      ��  �� BeginStoryboardName$	 bsb��6   5      .��5      � .+�5   
   �� -   5        $��_button��5      $��40��6    $	��120��6,   $��Start��68   $
��Left=�6J   $	��Top=�6e   6   �� 5   
   -   5        $��_button1��5      $��40��6!   $	��120��6-   $
��Stop��69   $��Right=�6J   $	��Top=�6f   6�   �� 5    
   -   5        $��_border��5       $	��100��6    $	��100��6,   
��� ��� J� RenderTransformOrigin$ .5,.5&�6M   z�5!      V� 5#      .~�5%      ��  �� Angle$ 45Q�6&   63   �� 5)       �� CenterY$ 0Q�6%    �� CenterX$ 0Q�61    �� ScaleX$ 1Q�6=    �� ScaleY$ 1Q�6H   6U   �� 5+       �� AngleX$ 30Q�6$    �� AngleY$	 -30Q�60   6?   5,      5.      �� 5/   
   $y�Hidden=�6   .+�50      �� $��20��6   $��20��6    
�������6E   �� 51      $��20��6   $��20��6    
�������6H   �� 52      $��20��6   $��20��6    
����� � � LayoutTransform 53      �� 54      $ 45Q�6&   56      �� 57      $��20��6   $��20��6    
������6F   �� 58      $��20��6   $��20��6    
�������6A   �� 59      $��20��6   $��20��6    
�������z�5:      �� 5;      $ 45Q�6&   5=      �� 5>      $��20��6   $��20��6    
���   �6A   5?      5@      5A      