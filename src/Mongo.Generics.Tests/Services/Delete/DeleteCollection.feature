Feature: Delete from a Collection - Service

Scenario: Can DeleteAsync
    Given a WriteService of PersonEntity
    And a collection of 10 persons exist
    When calling the DeleteAsync method of the WriteService
    Then the count of the collection of persons is equal to 9
