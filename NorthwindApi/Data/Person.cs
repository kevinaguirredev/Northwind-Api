using System;
using System.ComponentModel.DataAnnotations;

public class Person
{
    public int PersonId { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }

    [StringLength(100)]
    public string MiddleName { get; set; }

    [StringLength(100)]
    public string LastName { get; set; }

    [StringLength(20)]
    public string Suffix { get; set; }

    [StringLength(20)]
    public string Salutation { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [StringLength(75)]
    public string PersonType { get; set; }

    

}