Feature:  Create a Collection - Service

  Scenario: Can CreateAsync
    Given a WriteService of PersonEntity
    And 1 PersonEntity
    When calling the CreateAsync method of the WriteService
    Then the collection of persons is created
    And the count of the collection of persons equals 1
