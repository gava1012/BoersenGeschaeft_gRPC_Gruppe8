using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boerse;
using BoersenGeschaeft;
using Grpc.Core;


namespace BoersenGeschaeftServer
{
    public class BoersenGeschaeftImpl : Boerse.BoersenGeschaeft.BoersenGeschaeftBase
    {
        readonly List<BoerseResponse> boersen;
        
        public BoersenGeschaeftImpl(List<BoerseResponse> boersen)
        {
            this.boersen = boersen;
        }

        public override Task<BoerseResponse> GetBoerse(BoerseRequest request, ServerCallContext context)
        {
            return Task.FromResult(CheckBoerse(request));
        }

        public override async Task ListBoersen(BoerseRequest2 request, IServerStreamWriter<BoerseResponse> responseStream, ServerCallContext context)
        {
            var responses = boersen.FindAll( (boerse) => boerse.Exists() && request.Minuten < getMinutesDifference(boerse.Time) && 0 < getMinutesDifference(boerse.Time));
            foreach (var response in responses)
            {
                await responseStream.WriteAsync(response);
            }
        }



    public static double getMinutesDifference(string s1)
    {

      string[] subs1 = s1.Split('-');
     
      DateTime d1 = new DateTime(Convert.ToInt32(subs1[2]), Convert.ToInt32(subs1[1]), Convert.ToInt32(subs1[0]), Convert.ToInt32(subs1[3]), Convert.ToInt32(subs1[4]),0);
      DateTime d2 = DateTime.Now;

      TimeSpan t = d1 - d2;

      return t.TotalMinutes;

    }

    private BoerseResponse CheckBoerse(BoerseRequest location)
        {
            var result = boersen.FirstOrDefault((boerse) => boerse.TradeNummer.Equals(location.TradeNummer));
            if (result == null)
            {
                return new BoerseResponse { Bezeichnung = "", TradeNummer = ""};
            }
            return result;
        }
    }
}
