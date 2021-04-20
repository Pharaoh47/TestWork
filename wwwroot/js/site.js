// SPA primary js file
// we use knockout https://knockoutjs.com/

// it is for format birthday to beauty stance
var readableDate = function(dateString) {
    var self = this;
    let date = new Date(dateString);
    return date.toLocaleDateString("ru-RU");
};

// it is from formating date to format what knockout understand
function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();
    if (month.length < 2) 
        month = '0' + month;
    if (day.length < 2) 
        day = '0' + day;
    return [year, month, day].join('-');
}

// primary viewmodel
function PhonebookViewModel() {
    // typical for knockout
    var self = this;

    // two main arrays
    self.persons = ko.observableArray([]);
    self.phones = ko.observableArray([]);

    // fields for insertion forms
    self.newPersonName = ko.observable();
    self.newPersonBirthDay = ko.observable(formatDate(Date.now()));
    self.newPhone = ko.observable();
    self.newPhoneError = ko.observable(false);
    self.newPersonError = ko.observable(false);

    // fields for edit form
    self.editPersonId = ko.observable(0);
    self.editPersonName = ko.observable();
    self.editPersonBirthDay = ko.observable();

    // marker for phone numbers display
    self.showPhonesId = ko.observable(0);
    

    // an create person web api call
    self.addPerson = function() {
        // create object
        var person = {
            "name": self.newPersonName(),
            "birthDay": self.newPersonBirthDay(),
        };
        // ajax call to CRUD
        $.ajax("/api/person", {
            data: ko.toJSON(person),
            type: "post", contentType: "application/json",
            // reread list on success
            success: function(result) { 
                $.getJSON("/api/person/list", function(data) {
                    self.persons(data);
                });
            },
            // show message on error
            error: function(result){
                self.newPersonError(true);
            }
        });
        // clear from fields for new insertion
        self.newPersonName("");
        self.newPersonBirthDay(formatDate(Date.now()));
    };

    // delete person web api call
    self.deletePerson = function(person) {
        $.ajax("/api/person/" + person.id, {
            type: "delete", contentType: "application/json",
            // reread list on success
            success: function(result) { 
                $.getJSON("/api/person/list", function(data) {
                    self.persons(data);
                });
            }
        });  
    };

    // change stance of edit person block
    self.editPerson = function(person) {
        // set person id
        self.editPersonId(person.id);
        // and fill form
        self.editPersonName(person.name);
        self.editPersonBirthDay(formatDate(person.birthDay));
    };
    self.cancelEditPerson = function(person) {
        // unset person id
        self.editPersonId(0);
    };

    // update person web api call
    self.updatePerson = function() {
        $.ajax("/api/person/" + self.editPersonId(), {
            // format object fields
            data: ko.toJSON({
                "id": self.editPersonId(),
                "name": self.editPersonName(),
                "birthDay": self.editPersonBirthDay()
            }),
            type: "put", contentType: "application/json",
            // reread persons on success
            success: function(result) { 
                $.getJSON("/api/person/list", function(data) {
                    self.persons(data);
                });
                // unshow error message
                self.newPersonError(false);
            },
            // show error message on error
            error: function(result){
                self.newPersonError(true);
            }
        });
        self.editPersonId(0);
    };

    // change phone list block visibility and request phones data from web api
    self.showPhones = function(person) {
        // set person id for viewing phones
        self.showPhonesId(person.id);
        // clear error flag
        self.newPhoneError(false);
        // catch up phone numbers
        $.getJSON("/api/phone/list/" + person.id, function(data) {
            self.phones(data);
        });        
    };

    // delete phone web api call
    self.deletePhone = function(phone) {
        $.ajax("/api/phone/" + phone.id, {
            type: "delete", contentType: "application/json",
            success: function(result) { 
                $.getJSON("/api/phone/list/" + self.showPhonesId() , function(data) {
                    // update phone list
                    self.phones(data);
                });
            }
        });
    };

    // create phone api call
    self.addPhone = function() {
        // format object
        var phone = {
            "number": self.newPhone(),
            "personId": self.showPhonesId(),
        };
        // ajax call
        $.ajax("/api/phone", {
            data: ko.toJSON(phone),
            type: "post", contentType: "application/json",
            // fill phone numbers on success
            success: function(result) { 
                $.getJSON("/api/phone/list/" + self.showPhonesId() , function(data) {
                    self.phones(data);
                });
                // clear error flag
                self.newPhoneError(false);
            },
            // show error on validation fails
            error: function(result){
                self.newPhoneError(true);
            }
        });
        // clear field for new additions
        self.newPhone("");
    };

    // initial fill of persons list
    $.getJSON("/api/person/list", function(data) {
        self.persons(data);
    });
}

// initial knockout bind
ko.applyBindings(new PhonebookViewModel());