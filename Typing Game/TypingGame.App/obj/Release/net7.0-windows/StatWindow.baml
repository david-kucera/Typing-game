   M S B A M L   `   `   `  ���� I  ETypingGame.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TypingGame
TypingGame      TypingGame.App.StatWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:TypingGame     -     �Name$  StatWindow1�� � Title$ Your game stats��$	��450��$	��800�� � 
ResizeMode$ CanMinimize=� � Topmost$ False��.��� .+��� $��Summary of the game��$
��Left=�$��45��$��	35,35,0,0q�$	��Top=�$	��265��$��24(�$
��Bold"��� $��Total time of typing: ��$
��Left=�$��35��$��
35,100,0,0q�$	��Top=�$	��155��$��16(��� $#��Total number of chars typed: ��$
��Left=�$��35��$��
35,150,0,0q�$	��Top=�$	��215��$��16(��� $��Number of errors: ��$
��Left=�$��35��$��35,0,0,0q�$��Center=�$	��145��$��16(��� $/��)Average characters per minute (rounded): ��$
��Left=�$��35��$��
35,249,0,0q�$	��Top=�$	��310��$��16(��� $+��%Average words (4.7 char) per minute: ��$
��Left=�$��40��$��
35,304,0,0q�$	��Top=�$	��275��$��16(��� -    ��Name$ TextBlockLevel��$
��Left=�$��55��$��
35,349,0,0q�$
��Wrap=�$�� ��$	��Top=�$	��735��${�False��$��16(�$��Italic$�$��2���� -    ��Name$ TextBoxTotalTimeOfTyping��$
��Left=�$��35��$��355,100,0,0q� �� TextWrapping$
 Wrap=�$��Total_time_of_typing��$	��Top=�$	��250��$��16(�$
��Bold"� �� TextAlignment$ Center=��� -   $ TextBoxTotalNumberOfChars��$
��Left=�$��35��$��355,150,0,0q�$
 Wrap=�$��Total_number_of_chars��$	��Top=�$	��250��$��16(�$
��Bold"�$ Center=��� -   $ TextBoxNumberOfErrors��$
��Left=�$��35��$��	355,0,0,0q�$
 Wrap=�$��Number_of_errors��$��Center=�$	��250��$��16(�$ Center=�$
��Bold"��� -   $ TextBoxAvgCpm��$
��Left=�$��35��$��355,249,0,0q�$
 Wrap=�$��Avg_cpm��$	��Top=�$	��250��$ Center=�$��16(�$
��Bold"��� -   $ TextBoxAvgWpm��$
��Left=�$��30��$��355,304,0,0q�$
 Wrap=�$��Avg_wpm��$	��Top=�$	��250��$ Center=�$��16(�$
��Bold"��� -   $��
Play again��$
��Left=�$��44��$��630,124,0,0q�$	��Top=�$	��130���� -	   $��
Show stats��$
��Left=�$��44��$��630,200,0,0q�$	��Top=�$	��130��