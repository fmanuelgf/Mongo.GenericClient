Feature: Delete from a Collection - Repository

Scenario: Can DeleteOneAsync
    Given a GenericRepository of PersonEntity
    And a collection of 10 persons exist
    When calling the DeleteOneAsync method of the repository collection
    Then the count of the collection of persons equals 9
