   M S B A M L   `   `   `  ����B  >ChatBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ChatBox.MVVM.ViewChatBox.MVVM.View  5   	   !    ChatBox.MVVM.View.LogWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ChatBox.MVVM.View     5   	     � Title$  	LogWindow��5   	   $	��474��6   $	��816��6(   .��5	      � -   5         �Name$ WindowContent��5	      .+�5
   
   �� -   5         ��Name$ UserName��5
      $��Center=�5      $��	0,145,0,0q�6/    �� TextWrapping$
 Wrap=�6B   $	��Top=�6V   $	��136��6n   $��28��6z   ��5
   $   �� *LoginVM.Username+ �� UpdateSourceTrigger$ PropertyChanged=�5   z   �� 5   
   -   5        $ UserPassword��5      $��Center=�5      $
 Wrap=�6/   $��Center=�6D   $	��135��6_   $��24��6k   ��5   (   �� *LoginVM.Password+$ PropertyChanged=�5   k   �� 5   
   -   5         ��Name$ LoginButton��5      $��Login��6&   $
��Left=�5      $��373,279,0,0q�6,   $	��Top=�6A    J� RenderTransformOrigin$ 0.147,-1.802&�6Y   ��5      �� *ConnectToServerCommand+$ PropertyChanged=�5   Y   �� 5   
   -   5        $ testBut��5      $��TESSTTT��6"   $
��Left=�5      $��629,342,0,0q�6,   $	��Top=�6A   5      5      