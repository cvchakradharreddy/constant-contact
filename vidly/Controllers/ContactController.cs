using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTCT.Components;
using CTCT.Components.Contacts;
using CTCT.Services;
using Newtonsoft.Json;
using vidly.Models;
using vidly.Services;

namespace vidly.Controllers
{
    public class ContactController: Controller
    {
          MyContactService myContactService;
          ContactService contactService;

          public ContactController ()
          {
              myContactService = new MyContactService();
              contactService = myContactService.getContactService();
          }

        // GET: Contact
        // Lists all the contacts
        [Route("Home/Contact")]
        public ActionResult Index()
        {
            DateTime modifiedSince = new DateTime(2018, 1, 1);
            ResultSet<Contact> contactResults = contactService.GetContacts(modifiedSince);

            IList<Contact> contacts = contactResults.Results;
            List<MyContact> myContacts = new List<MyContact>();
            List<MyContact> myActiveContacts = new List<MyContact>();
            foreach (var contact in contacts)
            {
                var serializedParent = JsonConvert.SerializeObject(contact);
                MyContact con = JsonConvert.DeserializeObject<MyContact>(serializedParent);
                if (con.Status == "ACTIVE") 
                {
                    myActiveContacts.Add(con);
                }
                myContacts.Add(con);
            }

           /* foreach (var con in myContacts) {
                c = c + con.FirstName;
            }
            return Content("The contacts are " + c); */
            return View(myActiveContacts);
        }

       // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            Contact contact;
            try 
	        {	        
		         contact = contactService.GetContact(id.ToString());
	        }
	        catch (Exception)
	        {
		        return Content("No results");
	        }
            var serializedParent = JsonConvert.SerializeObject(contact);
            MyContact myContact = JsonConvert.DeserializeObject<MyContact>(serializedParent);
            return View(myContact);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName, LastName, EmailAddresses")] MyContact myContact)
        {
         
            ContactList myContactList = new ContactList();
            myContactList.Id = "1977064675";
            myContactList.Name = "General Interest";
            myContact.Lists.Add(myContactList);
            if (ModelState.IsValid)
            {
               Contact newContact = contactService.AddContact(myContact, false); 
                return RedirectToAction("Index");
            }

            return View(myContact);
        }

            // GET: Contact/Edit/5
            public ActionResult Edit(int? id)
            {
                Contact contact;
                if (id != null)
                {
                    contact = contactService.GetContact(id.ToString());
                    var serializedParent = JsonConvert.SerializeObject(contact);
                    MyContact myContact = JsonConvert.DeserializeObject<MyContact>(serializedParent);
                    return View(myContact);
                }
                return Content("Contact id is null");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "Id,FirstName, LastName, EmailAddresses")] MyContact myContact)
            {
                myContact.Status = "ACTIVE";
                Contact newContact = contactService.UpdateContact(myContact, false); 
                return RedirectToAction("Index");
            }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
           Contact contact;
           if(id != null)
           {
               contact = contactService.GetContact(id.ToString());
               var serializedParent = JsonConvert.SerializeObject(contact);
               MyContact myContact = JsonConvert.DeserializeObject<MyContact>(serializedParent);
               return View(myContact);
           }
           return Content("Contact id is null");
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id != null)
            {
                Contact contact = contactService.GetContact(id.ToString());
                var deleted = contactService.DeleteContact(id.ToString());
                var deletedFromLists = contactService.DeleteContactFromLists(contact);
            }
            return RedirectToAction("Index");
        }
       
    }
}