   M S B A M L   `   `   `  ����D  @Watermark, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Watermark	Watermark  5   	       Watermark.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Watermark     5   	   -   5          �Name$  
WindowMain��5   	    � Title$ 	Watermark��5   	   $	��613��6   $
��1200��6(    � 
ResizeMode$ NoResize=�65   ��5	      �� 5
   
   .��5      � ��5   
   �� 5       �� ImageSource$6 0pack://siteoforigin:,,,/Resources/background.png��6    j� Stretch$ UniformToFill=�6X   .+�5   
   �� -   5         ��Name$ setImage_button��5      $��	Add image��6*   $
��Left=�6>   $	��Top=�6Y   $	��417��6q   $��	37,34,0,0q�6}   $��52��6�   6�   �� 5   
   -   5        $ setWatermark_button��5      $��	Watermark��6.   $
��Left=�6B   $	��Top=�6]   $	��417��6u   $��
37,296,0,0q�6�   $��53��6�   6�   �� 5   
   -   5         ��Name$ watermark_view��5      $
��Left=�6,   $	��182��6G   $��
37,368,0,0q�6T   $	��Top=�6h   $	��284��6�   �� 5   
   -   5        $ result_view��5      ���	#FFB0B0B0$	��360��6:   $��
531,34,0,0q�6G   
���   �$	��Top=�6j   $
��Left=�6�   $	��640��6�   �� 5   
   -   5         ��Name$ canvas��5      $
��Left=�6!   $	��360��6<   $��
531,34,0,0q�6I   $	��Top=�6]   $	��640��6u   �� 5   
   -   5        $ 
image_view��5      ���	#FFF4F4F5$
��Left=�69   $	��149��6T   $��274,627,0,-192q�6a   
���   �$	��Top=�6�   $	��206��6�   �� 5   
   -   5        $ save_button��5      $��Export��6&   $
��Left=�67   $��442,450,0,0q�6R   $	��Top=�6g   $	��252��6   $��80��6�   $��20(�6�   �� 5   
   -	   5         ��Name$ 
DebugLabel��5      $��Label��6$   $
��Left=�64   $	��Top=�6O   	 J� RenderTransformOrigin$	 0.491,0.485&�6g   $��442,390,0,0q�6�   $��32��6�   �� 5   
   -
   5        
 ��Name$
 listView��5      $
��Left=�6%   $	��121��6@   $��511,627,0,-164q�6M   $	��Top=�6e   $	��417��6}    �� View 5      �� 5      .P�5      �� 5      5      �� 5    
   -   5        $��scrollviewer��5       $
��Left=�6+   $	��182��6F   $��	37,91,0,0q�6S   $	��Top=�6f   $	��417��6~   .��5!      �� .+�5"      �� -   5        $��ImagesListViewer��5"      ��67   �� *	imagelist+��5#      �� 5$      .R�5%      � $��0,0,0,5q�6#   .+�5&   "   �� ��6(   �� *name+6C   �� 5'   "   $	��100��6@   ��6(   �� *path+6@   5(      5)      5+      5-      5.      5/      50      