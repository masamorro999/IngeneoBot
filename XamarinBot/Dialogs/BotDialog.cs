using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System.Collections;

//Este es un demo para demostrar la funcionalidad de los bot par la resolucion y atencion de solicitudes automatizadas,
//tambien el poder y las ventajas que representan para solucionar problemas comunes en atencion al usuario y ventas de las compañias.

 /*+++++++ Desarrollado por Mateo Sierra para Ingeneo S.A.S 11 de septiembre 2017 ++++++++++*/

namespace XamarinBot.Dialogs
{
    //Datos de la app en la plataforma LUIS, primero appId, luego la key
    [LuisModel("df23a427-9140-4bce-ac25-278a64491bdb", "8ae7f6dd95d343b2badd081bb85771d5")]
    [Serializable]
    public class BotDialog : LuisDialog<object>
    {
        public BotDialog(params ILuisService[] services) : base(services)
        {
        }

        [LuisIntent("None")]
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Lo siento, mensaje no reconocido");
            context.Wait(MessageReceived);
        }

        [LuisIntent("AcercaDe")]        
        public async Task AcercaDe(IDialogContext context, LuisResult result)
        {            
            await context.PostAsync("Para ver informacion relacionada a tu cuenta y planes disponibles digita tu numero de cedula");
            context.Wait(MessageReceived);
        }   

        [LuisIntent("Busqueda")]
        public async Task Busqueda(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"La busqueda del Numero de cedula {result.Query} tiene los siguientes paquetes disponibles: ");
            var resultMessage = context.MakeMessage();
            
            resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            resultMessage.Attachments = new List<Attachment>()
            {
                new HeroCard
                {
                    Title = "Plan de Telefonia GOLD",
                    Subtitle = "Voz y Datos Ilimitados nacional + Internacional",
                    Text = "Plan de telefonia para usuarios VIP. Promocion valida por 2 años desde el momento de la activacion",
                    Images = new List<CardImage> { new CardImage("https://image.ibb.co/cd1nKF/Gold_Black.png") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.ImBack, "Activar Plan GOLD", value: "plan gold"),
                        new CardAction(ActionTypes.ImBack, "Volver al Menu", value: "menu")
                    }
                }.ToAttachment(),

                new HeroCard
                {
                    Title = "Plan de Telefonia Platino",
                    Subtitle = "500 minutos + 2000 megas de datos",
                    Text = "Plan de telefonia para usuarios premium. Promocion valida por 1 años desde el momento de la activacion",
                    Images = new List<CardImage> { new CardImage("https://image.ibb.co/i1n2KF/Silver_Black.png") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.ImBack, "Activar Plan Platino", value: "plan platino"),
                        new CardAction(ActionTypes.ImBack, "Volver al Menu", value: "menu")
                    }
                }.ToAttachment(),

                new HeroCard
                {
                    Title = "Plan de Telefonia Bronce",
                    Subtitle = "100 minutos + 500 megas de datos",
                    Text = "Plan de telefonia para usuarios premium. Promocion valida por 1 años desde el momento de la activacion",
                    Images = new List<CardImage> { new CardImage("https://image.ibb.co/nnxKzF/Bronce_Black.png") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.ImBack, "Activar Plan Bronce", value: "plan bronce"),
                        new CardAction(ActionTypes.ImBack, "Volver al Menu", value: "menu")
                    }
                }.ToAttachment()
            };
            await context.PostAsync(resultMessage);
            context.Wait(MessageReceived);
            //await context.PostAsync($"Puedes activar tu paquete ingresando en la opcion 'Activar Promocion' o escribiendo la palabra 'Plan' seguido del nombre de la promocion. Ejempo:'Plan Gold' ");
        }

        [LuisIntent("Plan Bronce")]
        public async Task PlanBronce(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Wow, felicidades!!! acabas de activar el paquete BRONCE, recuerda que tiene un costo mensual de $25.000 y una duracion de 1 año a partir del momento de la activacion. ¡Que buena energia! :D");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Plan Platino")]
        public async Task PlanPlatino(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Wow, felicidades!!! acabas de activar el paquete PLATINO, recuerda que tiene un costo mensual de $50.000 y una duracion de 1 año a partir del momento de la activacion. ¡Que super energia! :D");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Plan Gold")]
        public async Task PlanGold(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Wow, felicidades!!! acabas de activar el paquete GOLD, recuerda que tiene un costo mensual de $75.000 y una duracion de 2 años a partir del momento de la activacion. ¡Que SUPER MEGA BUENA ENERGIA! ;)");
            context.Wait(MessageReceived);
        }

        [LuisIntent("QuienSoy")]
        public async Task QuienEres(IDialogContext context, LuisResult result)
        {            
            var resultMessage = context.MakeMessage();            
            resultMessage.Attachments = new List<Attachment>()
            {
                new HeroCard
                {
                    Title = "Ingeneo S.A.S",
                    //Subtitle = "Ingeneo más de 15 años integrando soluciones de TI alrededor del mundo",
                    Text = "Soy IngeneoBot, un asistente en servicio al cliente y ventas en canales de comunicacion online, puedes ponerte en contacto con mis creadores o conocer mas de nosotros en:",
                    Images = new List<CardImage> { new CardImage("https://image.ibb.co/nr5R3a/ingeneo_Splsh.png") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.OpenUrl, "Quienes Somos", value: "http://www.ingeneo.com.co"),
                        new CardAction(ActionTypes.ImBack, "Volver al Menu", value: "menu")
                    },
                }.ToAttachment()
            };
            await context.PostAsync(resultMessage);
            context.Wait(MessageReceived);           
        }

        [LuisIntent("promociones")]
        public async Task Boton(IDialogContext context, LuisResult result)
        {
            var resultMessage = context.MakeMessage();
            //resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            resultMessage.Attachments = new List<Attachment>()
            {
                new HeroCard
                {
                    Title = "Super iPromocion",
                    Subtitle = "Iphone 7 + plan de voz y datos ilimitados",                    
                    Images = new List<CardImage> { new CardImage("https://store.storeimages.cdn-apple.com/4662/as-images.apple.com/is/image/AppleInc/aos/published/images/i/ph/iphonese/gallery1/iphonese-gallery1-2016?wid=835&hei=641&fmt=jpeg&qlt=95&op_sharpen=0&resMode=bicub&op_usm=0.5,0.5,0,0&iccEmbed=0&layer=comp&.v=1480454457897") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.OpenUrl, "Comprar", value: "http://www.ingeneo.com.co"),
                        new CardAction(ActionTypes.OpenUrl, "Ver Mas Promociones", value: "http://www.ingeneo.com.co"),
                        new CardAction(ActionTypes.ImBack, "Volver al Menu", value: "menu")
                    },
                }.ToAttachment()
            };            
            await context.PostAsync(resultMessage);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Hola")]
        public async Task Hola(IDialogContext context, LuisResult result)
        {
            var resultMessage = context.MakeMessage();            
            resultMessage.Attachments = new List<Attachment>()
            {
                new HeroCard
                {
                    Title = "Hola!",
                    Subtitle = "Puedo darte informacion de una de las siguientes opciones",                    
                    Images = new List<CardImage> { new CardImage("https://image.ibb.co/nr5R3a/ingeneo_Splsh.png") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.ImBack, "Promociones", value: "promociones"),
                        new CardAction(ActionTypes.ImBack, "Mis Planes", value: "mis planes"),
                        new CardAction(ActionTypes.ImBack, title: "Quien Soy?", value: "quien eres?"),
                        new CardAction(ActionTypes.OpenUrl, title: "Mi cuenta", value: "http://www.ingeneo.com.co")                        
                    },
                }.ToAttachment()
            };            
            await context.PostAsync(resultMessage);
            context.Wait(MessageReceived);
        }
    }
}