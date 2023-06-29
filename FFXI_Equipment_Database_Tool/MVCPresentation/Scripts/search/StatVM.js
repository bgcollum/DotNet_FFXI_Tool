$(function () {

    var $list = $('#aliasList');

    $input.on('input', function () {

        var searchTerm = $input.val().trim().toLowerCase();

        $list.find('.alias').each(function () {
            var $item = $(this);
            var alias = $item.text().toLowerCase();
            if (alias.includes(searchTerm)) {
                $item.show();
            } else {
                $item.hide();
            }
        });
    });
});