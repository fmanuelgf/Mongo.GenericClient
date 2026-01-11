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

  Scenario Outline: Can DeleteAsync - Array
    Given a WriteService of PersonEntity
    And a collection of 10 persons exist
    When calling the DeleteAsync an array of 7 IDs as <argType> method of the WriteService
    Then the count of the collection of persons equals 3

    Examples:
      | argType  |
      | ObjectId |
      | string   |

  Scenario Outline: Can DeleteAsync - List
    Given a WriteService of PersonEntity
    And a collection of 10 persons exist
    When calling the DeleteAsync a list of 7 IDs as <argType> method of the WriteService
    Then the count of the collection of persons equals 3

    Examples:
      | argType  |
      | ObjectId |
      | string   |

  Scenario Outline: Cannot DeleteAsync - Invalid Id
    Given a WriteService of PersonEntity
    And a collection of 10 persons exist
    When calling the DeleteAsync foo method of the WriteService
    Then an Exception is thrown
    And the Exception message is 'foo is not a valid ObjectId'

  Scenario Outline: Cannot DeleteAsync - Invalid Id - Array
    Given a WriteService of PersonEntity
    And a collection of 10 persons exist
    When calling the DeleteAsync an array of 7 IDs as string method of the WriteService with an invalid ID
    Then an Exception is thrown
    And the Exception message is 'foo is not a valid ObjectId'

  Scenario: Cannot DeleteAsync - Invalid Id - List
    Given a WriteService of PersonEntity
    And a collection of 10 persons exist
    When calling the DeleteAsync a list of 7 IDs as string method of the WriteService with an invalid ID
    Then an Exception is thrown
    And the Exception message is 'foo is not a valid ObjectId'