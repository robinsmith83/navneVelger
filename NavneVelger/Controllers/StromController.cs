using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using NavneVelger.Models;


namespace NavneVelger.Controllers
{
    public class StromController : Controller
    {
        [TempData]
        public string StatusMessage { get; set; }

        private const string ApiKey = "a1fa7a30d9b85b5a85054908bb01316e1d1f54c571a26aacb9f0f6681b01126d";

        public async Task<IActionResult> Index()
        {
            GraphQLClient client = new GraphQLClient(@"https://api.tibber.com/v1-beta/gql");
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {ApiKey}");




            StromViewModel model = new StromViewModel
            {
                StatusMessage = StatusMessage
            };

            GraphQLRequest request = new GraphQLRequest
            {
                Query = @"
                {
                viewer {
                    homes {
                      timeZone
                      address {
                        address1
                        postalCode
                        city
                        country
                      }
                      owner {
                        firstName
                        lastName
                      }
                      consumption(resolution: MONTHLY, last: 10) {
                        nodes {
                          from
                          to
                          totalCost
                          unitCost
                          unitPrice
                          unitPriceVAT
                          consumption
                          consumptionUnit
                        }
                      }
                    time:consumption(resolution: HOURLY, last: 24) {
                            nodes {
                                from
                                to
                                totalCost
                                unitCost
                                unitPrice
                                unitPriceVAT
                                consumption
                                consumptionUnit
                            }
                            }
                      currentSubscription {
                        priceInfo {
                          current {
                            total
                            energy
                            tax
                            startsAt
                          }
                        }
                      }
                    }
                  }                
                }"
            };

            var response = await client.PostAsync(request).ConfigureAwait(false);

            if (response.Errors == null)
            {
                model.Navn = response.Data.viewer.homes[0].owner.firstName + " " + response.Data.viewer.homes[0].owner.lastName;
                model.Adresse = response.Data.viewer.homes[0].address.address1;
                model.Postnummer = response.Data.viewer.homes[0].address.postalCode;
                model.Poststed = response.Data.viewer.homes[0].address.city;
                model.Land = response.Data.viewer.homes[0].address.country;

                model.StromprisTotal = response.Data.viewer.homes[0].currentSubscription.priceInfo.current.total;
                model.StromprisTotal *= 100;
                model.StromprisEnergi = response.Data.viewer.homes[0].currentSubscription.priceInfo.current.energy;
                model.StromprisEnergi *= 100;
                model.StromprisAvgift = response.Data.viewer.homes[0].currentSubscription.priceInfo.current.tax;
                model.StromprisAvgift *= 100;
                model.StromprisGjelderFra = response.Data.viewer.homes[0].currentSubscription.priceInfo.current.startsAt;

                model.Consumption = new List<Consumption>();
                for (int i = 0; i < response.Data.viewer.homes[0].consumption.nodes.Count; i++)
                {
                    model.Consumption.Add(
                        new Consumption
                        {
                            From = response.Data.viewer.homes[0].consumption.nodes[i].from,
                            To = response.Data.viewer.homes[0].consumption.nodes[i].to,
                            Cost = response.Data.viewer.homes[0].consumption.nodes[i].totalCost,
                            Price = response.Data.viewer.homes[0].consumption.nodes[i].unitPrice * 100,
                            ConsumptionInPeriod = response.Data.viewer.homes[0].consumption.nodes[i].consumption
                        }
                        );
                }

                model.ConsumptionHourly = new List<Consumption>();
                for (int i = 0; i < response.Data.viewer.homes[0].time.nodes.Count; i++)
                {
                    double price = 0;
                    if (response.Data.viewer.homes[0].time.nodes[i].unitPrice != null)
                        price = response.Data.viewer.homes[0].time.nodes[i].unitPrice * 100;

                    double cost = 0;
                    if (response.Data.viewer.homes[0].time.nodes[i].totalCost != null)
                        cost = response.Data.viewer.homes[0].time.nodes[i].totalCost;

                    double consumptionInPeriod = 0;
                    if (response.Data.viewer.homes[0].time.nodes[i].consumption != null)
                        consumptionInPeriod = response.Data.viewer.homes[0].time.nodes[i].consumption;

                    model.ConsumptionHourly.Add(
                        new Consumption
                        {
                            From = response.Data.viewer.homes[0].time.nodes[i].from,
                            To = response.Data.viewer.homes[0].time.nodes[i].to,
                            Cost = cost,
                            Price = price,
                            ConsumptionInPeriod = consumptionInPeriod
                        }
                        );
                }


                model.Consumption.Reverse();
                model.ConsumptionHourly.Reverse();

            }
            else
                StatusMessage = response.Errors[0].Message;                    


            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
