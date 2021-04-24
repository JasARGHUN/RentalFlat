redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51I8JZeAS6bVvU4dBPfh45uZm6yKyEcmHiLKxFoTFBEhXDziHlOVuqWxEe0vFDoAZJlXJIOhfDMr4MWTav2U0gPkz00ppIoWL8A');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};