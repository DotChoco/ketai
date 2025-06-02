using RDE.Structs.Enums;

namespace RDE.Models;

public abstract class FieldBorder: Component  {
    //Extra Cons 
    protected const char SPACE_CHAR = (char)32;
    protected const char CARRIAGE_RETURN = '\n';
    
    //Chars to make the Field
    protected char _topLeftCorner;
    protected char _topRightCorner;
    
    protected char _innerLine;
    protected char _leftSideLine;
    protected char _rightSideLine;
    protected char _intersectionLine;
    
    protected char _verticalLine;
    protected char _verticalBottonLine;
    protected char _verticalTopLine;
    
    protected char _bottonLeftCorner;
    protected char _bottonRightCorner;
    
    private void SetRoundedStyle()
    {
        _topLeftCorner = '\u256d'; //TLC
        _topRightCorner = '\u256e'; //TRC
        
        _innerLine = '\u2500'; // INNER LINE
        
        _leftSideLine = '\u251C';
        _rightSideLine = '\u2524';
        
        _verticalLine = '\u2502'; // VERTICAL BAR
        _verticalTopLine = '\u252C'; // VERTICAL TOP BAR
        _verticalBottonLine = '\u2534'; // VERTICAL BOTTON BAR
        _intersectionLine = '\u253C'; // INTERSECTION LINE
        
        _bottonLeftCorner = '\u2570'; //BLC
        _bottonRightCorner = '\u256f'; //BRC
        
    }
    
    private void SetBoxStyle()
    {
        _topLeftCorner = '\u250C'; //TLC
        _topRightCorner = '\u2510'; //TRC
        
        _innerLine = '\u2500'; // INNER LINE
        
        _leftSideLine = '\u251C';
        _rightSideLine = '\u2524';
        
        _verticalLine = '\u2502'; // VERTICAL BAR
        _verticalTopLine = '\u252C'; // VERTICAL TOP BAR
        _verticalBottonLine = '\u2534'; // VERTICAL BOTTON BAR
        _intersectionLine = '\u253C'; // INTERSECTION LINE
        
        _bottonLeftCorner = '\u2514'; //BLC
        _bottonRightCorner = '\u2518'; //BRC
        
    }
    
    private void SetBasicStyle()
    {
        _topLeftCorner = '+'; //TLC
        _topRightCorner = '+'; //TRC
        
        _innerLine = '-'; // INNER LINE
        _leftSideLine = '|';
        _rightSideLine = '|';
        
        _verticalLine = '|'; // VERTICAL BAR
        _verticalTopLine = '|'; // VERTICAL TOP BAR
        _verticalBottonLine = '|'; // VERTICAL BOTTON BAR
        _intersectionLine = '|';
        
        _bottonLeftCorner = '+'; //BLC
        _bottonRightCorner = '+'; //BRC
    }

    public void SetStyle(FieldStyles style)
    {
        if (style == FieldStyles.Basic)
            SetBasicStyle();
        else if (style == FieldStyles.Box)
            SetBoxStyle();
        else if (style == FieldStyles.Rounded)
            SetRoundedStyle();
    }

}