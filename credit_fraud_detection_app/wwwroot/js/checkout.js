




let totalAmount = document.getElementById("total-price");
const input = document.getElementById("card-number");


input.addEventListener(
    "input",
    () => (input.value = formatCardNumber(input.value.replaceAll(" ", "")))
);

function changeItemAmount(val) {
    if (typeof val !== "undefined") {
        totalAmount.innerHTML = val + "€";
        document.getElementById("purchase-value-form").value = val;
    }
}


const formatCardNumber = (number) =>
    number.split("").reduce((seed, next, index) => {
        if (index !== 0 && !(index % 4)) seed += " ";
        return seed + next;
    }, "");

function checkFormInputs() {
    let inputFilled = 0;
    let nameInput = document.getElementById("first-name-input");
    let lastNameInput = document.getElementById("last-name-input");
    let cardNumber = document.getElementById("card-number");
    let homeAddressInput = document.getElementById("home-LatLng");
    let lastPurchaseAdressInput = document.getElementById(
        "last-location-LatLng"
    );
    let currentAdressInput = document.getElementById("current-LatLng");
    let inputsObjects = [
        {
            input: nameInput.value,
            action: () => (nameInput.style.borderColor = "red"),
        },
        {
            input: lastNameInput.value,
            action: () => (lastNameInput.style.borderColor = "red"),
        },
        {
            input: cardNumber.value,
            action: () => (cardNumber.style.borderColor = "red"),
        },
        {
            input: homeAddressInput.value,
            action: () =>
            (document.getElementById("home-address").style.borderColor =
                "red"),
        },
        {
            input: lastPurchaseAdressInput.value,
            action: () =>
            (document.getElementById(
                "last-location-adress"
            ).style.borderColor = "red"),
        },
        {
            input: currentAdressInput.value,
            action: () =>
                alert(
                    "Molimo Vas dopustite pristup loakciji kako bi nastavili s transakcijom."
                ),
        },
    ];

    inputsObjects.forEach((element) => {
        if (element.input === null || element.input === "") {
            element.action();
        } else {
            inputFilled++;
        }
    });

    //If all inputs are filled countinue with checkup
    if (inputFilled === inputsObjects.length) {
        //Press post button and begin with checkup
        let postButton = document.getElementById("click-post");
        postButton.click();
    }
}

function SetInputBorderColor(obj) {
    if (obj.val !== "" || obj.val !== " ") {
        obj.style.borderColor = "lightgrey";
    }
}