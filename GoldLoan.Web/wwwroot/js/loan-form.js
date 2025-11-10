$(document).ready(function () {
    $('#LoanPlanId').on('change', function () {
        if ($(this).val()) {
            $('#manual-entry').hide();
        } else {
            $('#manual-entry').show();
        }
    });
});
