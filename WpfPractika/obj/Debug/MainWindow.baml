   M S B A M L   `   `   `  ����F  BWpfPractika, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfPractikaWpfPractika  5   	       WpfPractika.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:WpfPractika     5   	     � Title$  
MainWindow��5   	   $	��800��6   $
��1200��6)    � 
ResizeMode$ NoResize=�66   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      .+�5   
   �� -   5        $��	TableName��5      $
��Left=�6%   $��
471,40,0,0q�6@   $
��Wrap=�6T   $%��Название таблицы��6h   $	��Top=�6�   $��30(�6�   $��2��6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgItems��5      $��50,100,50,0q�6M      CanUserAddRows .  $	��210��6�      CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeRows .  $��2��5   �   $	��Top=�6�   �� 5   
   -   5        $#��Добавить запись��5      $��Center=�6+   $��	0,350,0,0q�6H   $	��Top=�6[   $��28(�6s   6�   �� 5   
   -   5        $��btDel��5      $!��Удалить запись��6   $��Center=�67   $��	0,350,0,0q�6T   $	��Top=�6g   $��28(�6   $��1��6�   {�.  6�   �� 5   
   -   5        $*��$Экспортировать в Word��5      $��Center=�61   $��	0,420,0,0q�6N   $	��Top=�6a   $��28(�6y   6�   �� 5   
   -   5        $+��%Экспортировать в Excel��5      $��Center=�62   $��	0,420,0,0q�6O   $	��Top=�6b   $��28(�6z   $��1��6�   �� 5   
   -   5        $��btVostFalseItemOpt��5      $/��)Востановить выбранное��6+   $��Center=�6K   $��	0,490,0,0q�6h   $	��Top=�6{   $��28(�6�   {�.  $y�Visible=�6�   6�   �� 5   
   -   5        $��btDelFasleItemOpt��5      $2��,Мнимо удалить выбранное��6*   $��Center=�6L   $��	0,490,0,0q�6i   $	��Top=�6|   $��28(�6�   {�.  $y�Visible=�6�   $��1��6�   �� 5   
   -	   5        $��lbNumberPage��5      $"��Номер страницы:��6$   $��Center=�6>   $	��Top=�6[   $��24(�6s   $��	0,560,0,0q�6�   $y�Visible=�6�   �� 5   
   -
   5        $��tbNumberPage��5      $��1��6&   $��Center=�6/   $	��Top=�6L   $��	0,560,0,0q�6d   $y�Visible=�6w   $��1��6�   $��24(�6�   $	��400��6�   $��Right=�6�   �� 5   
   -   5        $��lbCountLine��5      $@��:Количество записей на странице:��6#   $��Center=�6M   $	��Top=�6j   $��24(�6�   $��	0,630,0,0q�6�   $y�Visible=�6�   �� 5    
   -   5        $��tbCountLine��5       $��1��6%   $��Center=�6.   $	��Top=�6K   $��	0,630,0,0q�6c   $y�Visible=�6v   $��1��6�   $��24(�6�   $	��400��6�   $��Right=�6�   �� 5"   
   -   5        $��	btGetPage��5"      $'��!Получить страницу��6"   $��Center=�6>   $��	0,700,0,0q�6[   $	��Top=�6n   $��28(�6�   $y�Visible=�6�   $��2��6�   $
��1000��6�   6�   5#      5$      