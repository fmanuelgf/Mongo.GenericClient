Feature: Update a Collection - Service

Scenario Outline: Can ReplaceOneAsync
    Given a WriteService of PersonEntity
    And a collection of 1 persons exist
    And the PersonEntity.<field> is modified to <value>
    When calling the UpdateAsync method of the WriteService
    Then the person <field> in the collection equals <value>

    Examples:
        | field | value |
        | Name  | Tom   |
        | Age   | 30    |
