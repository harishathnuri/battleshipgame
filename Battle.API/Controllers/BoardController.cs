﻿using Battle.API.Factories;
using Battle.API.Filters;
using Battle.API.ViewModel;
using Battle.Domain;
using Battle.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Battle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    public class BoardController : Controller
    {
        private readonly IBoardRepository boardRepo;
        private readonly IBlockRepository blockRepo;
        private readonly ILogger<BoardController> logger;

        public BoardController(
            IBoardRepository boardRepo,
            IBlockRepository blockRepo,
            ILogger<BoardController> logger)
        {
            this.boardRepo = boardRepo;
            this.blockRepo = blockRepo;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult ApiBoardList()
        {
            logger.LogDebug($"Start - Request for boards");

            var boardsFromRepo = boardRepo.List();
            var response = new List<BoardResponse>();
            if (boardsFromRepo.Count() > 0)
            {
                response = boardsFromRepo
                    .Select(ResponseFactory.Create)
                    .ToList();
            }

            logger.LogDebug($"End - Request for boards");

            return Ok(response);
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(ValidateModelState))]
        [TypeFilter(typeof(ValidateBoardId))]
        public IActionResult ApiBoardGet(int id)
        {
            logger.LogDebug($"Start - Request for board {id}");

            var board = boardRepo.Get(id);
            var response = ResponseFactory.Create(board);

            logger.LogDebug($"End - Request for board {id}");

            return Ok(response);
        }

        [HttpPost]
        public ObjectResult ApiBoardPost()
        {
            logger.LogDebug("Start - Request for new board");

            var board = new Board();
            var boardFromRepo = boardRepo.Create(board);
            var blocks = Enumerable.Range(
                boardFromRepo.FirstBlockNumber,
                boardFromRepo.LastBlockNumber)
                .Select(n => new Block
                {
                    BoardId = boardFromRepo.Id,
                    Number = n
                })
                .ToList();
            var blocksFromRepo = blockRepo.CreateBlocksForBoard(blocks);

            boardFromRepo = boardRepo.Get(boardFromRepo.Id);
            var response = ResponseFactory.Create(boardFromRepo);

            logger.LogDebug("End - Request for new board");

            return CreatedAtAction(nameof(ApiBoardGet),
                new { id = boardFromRepo.Id }, response);
        }
    }
}