   M S B A M L   `   `   `  ����   TypingGame.App    TypingGame.App.MenuWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$
  Menu�� � Title$ Typing Game Menu��$	��460��$	��800�� � 
ResizeMode$ CanMinimize=�.���  � ColumnDefinitions �� $��3* ��� $
��153* ��� $��4* � � RowDefinitions �� $��2* ��� $
��215* �.+���    Welcome#���   $
��Left=�$��65��$��
172,55,0,0q�$	��Top=�$	��376��$��Center=�$��Center=�$
��Bold"�$��24(�$��1��$��1����   ChooseDictionary#���  $
��Left=�$��
75,136,0,0q�$	��Top=�$��Center=�$��Center=�$
��Bold"�$��1��$��1���� -    ��Name$ LanguageComboBox��$
��Left=�$��26��$��220,136,0,0q�$	��Top=�$	��280��$��1��$��1��.@���   SlovakB#���  ��   EnglishB#���  ��   ProgrammerB#���  ��   ChooseDifficulty#���  $
��Left=�$��
75,200,0,0q�$	��Top=�$
��Bold"�$��1��$��1��	� -    	�Name$ EasyRadioBox��  DifficultyEasy#���  $
��Left=�$��235,205,0,0q�$	��Top=�$��1��$��1�� d� 	IsChecked$
 TrueY�	� -   $ MediumRadioBox��  DifficultyMedium#���  $
��Left=�$��325,205,0,0q�$	��Top=�$��1��$��1��$ FalseY�	� -   $ HardRadioBox��  DifficultyHard#���  $
��Left=�$��435,205,0,0q�$	��Top=� J� RenderTransformOrigin$ 0.579,0.303&�$��1��$��1��$ FalseY��� $
��Left=�$��26��$��220,200,0,0q�
���   �$	��Top=�$	��280��$��1��$��1���� -   	 ��Name$	 StartTheGameButton�� 		 Start#��� 	 $
��Left=�$��297,265,0,0q�$	��Top=�$
��Bold"�$��50��$	��126��$��1��$��1���� -   $	 PlayOnConsoleButton�� 
 StartConsole#��� 
 $
��Left=�$��30��$��	0,390,0,0q�$	��Top=�$	��800��$��1��$��3��$��0��o� -   
 o�Name$
 	AboutMenu��$��3��$��0��$��	0,0,0,400q� J� 	AllowDrop$
 True��$��0��$��2��.@�m�   File#���  .@�m� -	     Exit#���  m�   Edit#���  .@�m� -
     BackgroundColor#���  m�  	 Stats#���  .@�m� -     	ShowStats#���  m� -     
ResetStats#���  m�   Language#���  .@�m� -     English#���  m� -    
 Slovak#���  m�   Help#���  .@�m� -     	AboutGame#���  m� -     	HowToPlay#���  