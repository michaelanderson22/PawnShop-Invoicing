   M S B A M L   `   `   `  ����H  DGroup Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Group_ProjectGroup_Project  5   	       Group_Project.wndSearch   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Group_Project     5   	     � Title$  	wndSearch��5   	   $	��450��6   $	��800��6(    � 	Resources 5	       �� Source�� 5
   
   $ ../Theme.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      $
��153* �6   �� 5      $	��64* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 
dgDataGrid��5      $
��Left=�6'   $	��150��6B   $��
65,171,0,0q�6O   $	��Top=�6c   $	��428��6{   $��2��6�      CanUserAddRows$ False��6�      
IsReadOnly$
 True��6�      AutoGenerateColumns$
 True��6�   5      �� 5   
   -   5         ��Name$ btnClear��5      $��Clear Filter��6#   $
��Left=�6:   $��30��6U   $��540,165,0,0q�6a   $	��Top=�6v   $��97��6�   6�   �� 5   
   -   5        $ 	btnSelect��5      $��Select Invoice��6$   $
��Left=�6=   $��30��6X   $��540,224,0,0q�6d   $	��Top=�6y   $��97��6�   6�   �� 5   
   -   5        	 ��Name$	 
lblInvoice��5      $��All Invoices��6$   $
��Left=�6;   $��24��6V   $��65,0,0,0q�6b   $��Center=�6t   $��87��6�   �� 5   
   -   5        
 ��Name$
 cbNumber��5      $
��Left=�6%   $��26��6@   $��
164,79,0,0q�6L   $	��Top=�6`   $��78��6x   6�   �� 5   
   -   5        $	 	lblNumber��5      $��Invoice Number:��6#   $
��Left=�6=   $��26��6X   $��	65,79,0,0q�6d   $	��Top=�6w   $��94��6�   �� 5   
   -   5        $	 lblDate��5      $��Invoice Date:��6!   $
��Left=�69   $��26��6T   $��
255,79,0,0q�6`   $	��Top=�6t   $��83��6�   �� 5    
   -   5        $
 cbDate��5       $
��Left=�6#   $��26��6>   $��
340,79,0,0q�6J   $	��Top=�6^   $��78��6v   6�   �� 5!   
   -	   5        $	 lblCosts��5!      $��Total Costs: ��6"   $
��Left=�6:   $��26��6U   $��
433,79,0,0q�6a   $	��Top=�6u   $��83��6�   �� 5"   
   -
   5        $
 cbCosts��5"      $
��Left=�6$   $��26��6?   $��
508,79,0,0q�6K   $	��Top=�6_   $��78��6w   6�   5%      5&      