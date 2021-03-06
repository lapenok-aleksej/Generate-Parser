using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GenerateParser.RunTime;


    using test;


class ExpParser : BaseParser
{
    public ExpParser(ExpLexer lexer) : base(lexer)
    {
    }

    public sNode s()
    {
        var token = lexer.GetToken();
        if (token.Id == 3 || token.Id == 2)
        {
            var text = "";
            var arg0 = mul();
            text += arg0.Text;
            var arg1 = MINUS();
            text += arg1.Text;
            var arg2 = sub();
            text += arg2.Text;
            var result = new sNode(text, null, arg0, arg1, arg2);
            result.mul = arg0;
            result.MINUS = arg1;
            result.sub = arg2;
            result.res=result.mul.res.Add(result.sub.res);
            token = lexer.GetToken();
            if (token.Id != -1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public subNode sub()
    {
        var token = lexer.GetToken();
        if (token.Id == 3 || token.Id == 2)
        {
            var text = "";
            var arg0 = mul();
            text += arg0.Text;
            var arg1 = sub2();
            text += arg1.Text;
            var result = new subNode(text, null, arg0, arg1);
            result.mul = arg0;
            result.sub2 = arg1;
            result.res=result.sub2.res.Sub(result.mul.res);
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 4)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public sub2Node sub2()
    {
        var token = lexer.GetToken();
        if (token.Id == 5)
        {
            var text = "";
            var arg0 = MINUS();
            text += arg0.Text;
            var arg1 = sub();
            text += arg1.Text;
            var result = new sub2Node(text, null, arg0, arg1);
            result.MINUS = arg0;
            result.sub = arg1;
            result.res=result.sub.res;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 4)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (true)
        {
            var text = "";
            var result = new sub2Node(text, null);
            result.res = new MyDouble(0);
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 4)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public mulNode mul()
    {
        var token = lexer.GetToken();
        if (token.Id == 3 || token.Id == 2)
        {
            var text = "";
            var arg0 = num();
            text += arg0.Text;
            var arg1 = mul2();
            text += arg1.Text;
            var result = new mulNode(text, null, arg0, arg1);
            result.num = arg0;
            result.mul2 = arg1;
            result.res=result.num.res.Mul(result.mul2.res);
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 5 && token.Id != 4)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public mul2Node mul2()
    {
        var token = lexer.GetToken();
        if (token.Id == 1)
        {
            var text = "";
            var arg0 = MUL();
            text += arg0.Text;
            var arg1 = mul();
            text += arg1.Text;
            var result = new mul2Node(text, null, arg0, arg1);
            result.MUL = arg0;
            result.mul = arg1;
            result.res=result.mul.res;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 5 && token.Id != 4)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (true)
        {
            var text = "";
            var result = new mul2Node(text, null);
            result.res=new MyDouble(1);
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 5 && token.Id != 4)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public numNode num()
    {
        var token = lexer.GetToken();
        if (token.Id == 3)
        {
            var text = "";
            var arg0 = LB();
            text += arg0.Text;
            var arg1 = sub();
            text += arg1.Text;
            var arg2 = RB();
            text += arg2.Text;
            var result = new numNode(text, null, arg0, arg1, arg2);
            result.LB = arg0;
            result.sub = arg1;
            result.RB = arg2;
            result.res = result.sub.res;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 1 && token.Id != 5 && token.Id != 4)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 2)
        {
            var text = "";
            var arg0 = NUM();
            text += arg0.Text;
            var result = new numNode(text, null, arg0);
            result.NUM = arg0;
            result.res=new MyDouble(result.NUM.Text);
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 1 && token.Id != 5 && token.Id != 4)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public PLUSNode PLUS()
    {
        if (lexer.GetToken().Id == 0)
        {
            var result = new PLUSNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public MULNode MUL()
    {
        if (lexer.GetToken().Id == 1)
        {
            var result = new MULNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public NUMNode NUM()
    {
        if (lexer.GetToken().Id == 2)
        {
            var result = new NUMNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public LBNode LB()
    {
        if (lexer.GetToken().Id == 3)
        {
            var result = new LBNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public RBNode RB()
    {
        if (lexer.GetToken().Id == 4)
        {
            var result = new RBNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public MINUSNode MINUS()
    {
        if (lexer.GetToken().Id == 5)
        {
            var result = new MINUSNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }
}
