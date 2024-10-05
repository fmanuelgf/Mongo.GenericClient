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

Scenario Outline: Cannot DeleteAsync - Invalid Id or Name
    Given a WriteService of PersonEntity
    And a collection of 10 persons exist
    When calling the DeleteAsync foo method of the WriteService
    Then an Exception is thrown
    And the Exception message is 'foo is not a valid ObjectId'