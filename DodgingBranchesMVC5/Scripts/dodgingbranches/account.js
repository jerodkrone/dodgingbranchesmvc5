dodgingbranches.account = (function(){

    this.registerExtLoginClick = function () {
        $('.extLoginBtn').bind('click', function (e) {
            e.preventDefault();
            $(this).siblings("[name='Provider']").val($(this).attr("name"));
            $('#extProviderForm').submit();
        })
    }

    return this;
}());