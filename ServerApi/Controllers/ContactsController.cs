﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServerApi.Models;
using ServerApp.Services;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private UserService _uservice;

        public ContactsController()
        {
            _uservice = new UserService();
        }

        // GET: Contacts
        [HttpGet]
        public IEnumerable<Contacts> Index()
        {
            return _uservice.GetContacts();
        }

        // GET: Contacts/Details/5
        [HttpGet("{id}")]
        public IActionResult /*Contacts*/ Details(string id)
        {
            if (!ContactsExists(id))
            {
                return BadRequest();
            }
            return Ok(_uservice.GetContacts().Where(x => x.Id  == id).FirstOrDefault());
        }


        [HttpPost]
        public IActionResult Create([Bind("Id,Name,Server")] Contacts contacts)
        {
            if (ContactsExists(contacts.Id))
            {
                return BadRequest();
            }
            contacts.LastDate = null;
            contacts.Last = null;
            _uservice.GetContacts().Add(contacts);
            return Ok();
        }



        // GET: Contacts/Details/5
        [HttpPut("{id}")]
        public IActionResult Edit(string id, [Bind("Name,Server")] Contacts contacts)
        {
            if (!ContactsExists(id))
            {
                return BadRequest();
            }
            foreach (var contact in _uservice.GetContacts())
            {
                if (contact.Id == id)
                {
                    contact.Name = contacts.Name;
                    contact.Server = contacts.Server;
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!ContactsExists(id))
            {
                return BadRequest();
            }
            foreach (var contact in _uservice.GetContacts())
            {
                if (contact.Id == id)
                {
                    _uservice.GetContacts().Remove(contact);
                    return Ok();
                }
            }
            return BadRequest();
        }

        // GET: Contacts/:id/messages 
        [HttpGet("{id}/messages")]
        //[Route("{id}/messages")]
        public IActionResult /*IEnumerable<Messages>*/ GetByIDMessages(string id)
        {
            List<Chats> chats = _uservice.GetMessages();
            List<Messages> messages = null;
            foreach (Chats chat in chats)
            {
                if (chat.Id == id)
                {
                    messages = chat.Messages;
                }
            }
            if (messages != null)
            {
                return Ok(messages);
            }
            
            return BadRequest();
        }

        // POST: Contacts/:id/messages 
        [HttpPost("{id}/messages")]
        //[Route("{id}/messages")]
        public IActionResult /*IEnumerable<Messages>*/ PostByIDMessages(string id ,[Bind("Id,Content,Created,Sent")] Messages message)
        {
            List<Chats> chats = _uservice.GetMessages();
            List<Messages> messages = null;
            foreach (Chats chat in chats)
            {
                if (chat.Id == id)
                {
                    chat.Messages.Add(message);
                }
            }
            if (messages != null)
            {
                return Ok(messages);
            }

            return BadRequest();
        }


        // GET: Contacts/:id/messages/:id2
        [HttpGet("{id}/messages/{idmessage}")]
        //[Route("{id}/messages")]
        public IActionResult /*IEnumerable<Messages>*/ GetMessage(string id, int idmessage)
        {
            List<Chats> chats = _uservice.GetMessages();
            List<Messages> messages = null;
            Messages message1 = null;
            foreach (Chats chat in chats)
            {
                if (chat.Id == id)
                {
                    messages = chat.Messages;
                }
            }
            if (messages == null)
            {
                return BadRequest();
            }

            
            foreach (Messages message in messages)
            {
                if (message.Id == idmessage)
                {
                    message1 = message;
                }
            }

            if (message1 != null)
            {
                return Ok(message1);
            }

            return BadRequest();

        }


        // PUT: Contacts/:id/messages/:id2
        [HttpPut("{id}/messages/{idmessage}")]
        //[Route("{id}/messages")]
        public IActionResult /*IEnumerable<Messages>*/ PutMessage(string id, int idmessage, [Bind("Id,Content,Created,Sent")] Messages message)
        {
            List<Chats> chats = _uservice.GetMessages();
            List<Messages> messages = null;
            int flag = 0;
            foreach (Chats chat in chats)
            {
                if (chat.Id == id)
                {
                    messages = chat.Messages;
                }
            }
            if (messages == null)
            {
                return BadRequest();
            }


            foreach (Messages mes in messages)
            {
                if (mes.Id == idmessage)
                {
                    mes.Sent = message.Sent;
                    mes.Id = message.Id;
                    mes.Created = message.Created;
                    mes.Content = message.Content;
                    flag = 1;
                }
            }

            if (flag == 0)
            {
                return BadRequest();
            }
            
            return Ok();

           

        }

        // DELETE: Contacts/:id/messages/:id2
        [HttpDelete("{id}/messages/{idmessage}")]
        public IActionResult /*IEnumerable<Messages>*/ DeleteMessage(string id, int idmessage, [Bind("Id,Content,Created,Sent")] Messages message)
        {
            List<Chats> chats = _uservice.GetMessages();
            List<Messages> messages = null;
            
            int flag = 0;

            foreach (Chats chat in chats)
            {
                if (chat.Id == id)
                {
                    messages = chat.Messages;
                }
            }
            if (messages == null)
            {
                return BadRequest();
            }


            foreach (Messages mes in messages)
            {
                if (mes.Id == idmessage)
                {
                    messages.Remove(mes);
                    flag = 1;

                }
            }

            if (flag == 0)
            {
                return BadRequest();
            }

            return Ok();

        }

        // GET: Contacts/Edit/5
        /*public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }
            return View(contacts);
        }*/

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Server,Last,LastDate")] Contacts contacts)
        {
            if (id != contacts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contacts.Id = id;
                    _context.Update(contacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactsExists(contacts.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
            }
            return BadRequest();
        }*/

        // GET: Contacts/Delete/5
        /* [HttpDelete("{id}")]
         public async Task<IActionResult> Delete(string id)
         {
             if (id == null || _context.Contacts == null)
             {
                 return NotFound();
             }

             var contacts = await _context.Contacts
                 .FirstOrDefaultAsync(m => m.Id == id);
             if (contacts == null)
             {
                 return NotFound();
             }

             return View(contacts);
         }*/

        // POST: Contacts/Delete/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'ServerAppContext.Contacts'  is null.");
            }
            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts != null)
            {
                _context.Contacts.Remove(contacts);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }*/

        private bool ContactsExists(string id)
        {
            return (_uservice.GetContacts()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
