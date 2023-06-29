$(document).ready(function () {
    // Get the list of StatVM objects
    var stats = @Html.Raw(Json.Encode(Model.Stats));
    console.log(stats);
    // Listen for changes to the search bar input field
    $('#search').on('input', function () {
        // Get the current value of the search bar
        var searchValue = $(this).val().toLowerCase();

        // Filter the list of StatVM objects based on the search value
        var filteredStats = stats.filter(function (stat) {
            // Check if the StatVM's Name property or any of its AliasList items contain the search value
            return stat.Name.toLowerCase().indexOf(searchValue) !== -1 ||
                stat.AliasList.some(function (alias) {
                    return alias.toLowerCase().indexOf(searchValue) !== -1;
                });
        });

        // Update the container that displays the list of StatVM objects with the filtered results
        var statList = $('#stat-list');
        statList.empty();
        filteredStats.forEach(function (stat) {
            var li = $('<li>');
            var h2 = $('<h2>').text(stat.Name);
            var ul = $('<ul>');
            stat.AliasList.forEach(function (alias) {
                // Check if the current alias contains the search value
                if (alias.toLowerCase().indexOf(searchValue) !== -1) {
                    $('<li>').text(alias).appendTo(ul);
                }
            });
            // Only add the StatVM to the list if at least one of its AliasList items matches the search value
            if (ul.children().length > 0) {
                li.append(h2).append(ul).append($('<p>').text('Value: ' + stat.Value)).appendTo(statList);
            }
        });
    });
});