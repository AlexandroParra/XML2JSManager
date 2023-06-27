/***************************************************************
*
* XML  
*
***************************************************************/
console.clear();
var response = xml2Json(responseBody);
var hoteles = response['env:Envelope']['env:Body']['ns0:availableHotelsByMultiQueryV22Response']['result']


console.log('Número de hoteles respondidos: ' + hoteles.totalRows);

/* 
//hoteles.availableHotels[0].roomCombinations[0].roomPrices[0].ratePlanCode;

var ratePlanCode = hoteles.availableHotels[0].roomCombinations[0].roomPrices[0].ratePlanCode;
pm.environment.set('ratePlanCode', ratePlanCode);
*/


//******************* Inicio de extracción de información mediante LOG 

for (var iHotel = 0; iHotel < hoteles['availableHotels'].length; iHotel++) {

    var hotel = hoteles['availableHotels'][iHotel];

    var hotelId = hotel.establishment.id;

    console.log('Hotel ID: ' + hotelId + ' con ' + hotel['roomCombinations'].length + ' combinaciones.');

    for (var iRoomCombination = 0; iRoomCombination < hotel['roomCombinations'].length; iRoomCombination++) {

        var roomCombination = hotel.roomCombinations[iRoomCombination];

        for (var iPrices = 0; iPrices < roomCombination['prices'].length; iPrices++) {

            var prices = roomCombination['prices'][iPrices];

            console.log('Precio Combinación ' + iRoomCombination + ': ' + prices.amount.value);

            //***************************  Inicio Información sobre el Rate  ***************************

            for (var iRoomPrices = 0; iRoomPrices < prices.roomPrices.length; iRoomPrices++) {

                var roomPrice = prices.roomPrices[iRoomPrices];

                console.log('Para habitación con ' + roomPrice.paxes + ' personas - RatePlanCode: ' + roomPrice.ratePlanCode);
            }

            //***************************  Fin Información sobre el Rate  ***************************
        }
    }
}

//******************* Fin de extracción de información mediante LOG **********/