Feature: Delete from a Collection - Service

Scenario Outline: Can DeleteAsync
    Given a WriteService of PersonEntity
    And a collection of 10 persons exist
    And 1 person has the Id xyz
    When calling the DeleteAsync xyz as <argType> method of the WriteService
    Then the count of the collection of persons equals 9

    Examples:
        | argType  | 
        | ObjectId | 
        | string   |
