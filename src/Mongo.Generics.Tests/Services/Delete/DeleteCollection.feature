Feature: Delete a Collection - Service

Scenario: Can DeleteAsync
    Given a WriteService of PersonEntity
    And a collection of 10 persons exist
    When calling the DeleteAsync method of the WriteService
    Then the collection of persons count equals 9
