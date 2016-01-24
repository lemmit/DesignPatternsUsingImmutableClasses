﻿using PatternLibrary.Handlers;

namespace PatternLibrary.ChainOfResponsibility
{
    public class ChainHandler : IHandler
    {
        private readonly IHandler Handler;
        private readonly ChainHandler NextHandler;

        public ChainHandler(IHandler handler)
        {
            Handler = handler;
        }

        public ChainHandler(IHandler handler, ChainHandler nextHandler)
        {
            Handler = handler;
            NextHandler = nextHandler;
        }

        public bool CanHandle(IRequest request)
        {
            return Handler.CanHandle(request) || 
                NextHandler != null ? NextHandler.CanHandle(request) : false;
        }

        public void Handle(IRequest request)
        {
            if (Handler.CanHandle(request))
            {
                Handler.Handle(request);
            }
            else NextHandler.Handle(request);
        }

        public ChainHandler SetNextHandler(ChainHandler nextHandler)
        {
            return new ChainHandler(Handler, nextHandler);
        }
    }
}