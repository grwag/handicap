import { Component, OnInit, Input, ViewChild, AfterViewInit } from '@angular/core';
import { Player } from '../shared/player';
import { GamesDataSource } from '../shared/dataSources/gamesDataSource';
import { MatPaginator, MatSort } from '@angular/material';
import { GameService } from '../shared/services/game.service';
import { Game } from '../shared/game';
import { merge } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-game-table',
  templateUrl: './game-table.component.html',
  providers: [GameService],
  styleUrls: ['./game-table.component.css']
})
export class GameTableComponent implements OnInit, AfterViewInit {
  @Input() player?: Player = null;

  totalGames: number;
  isPlayerGames: boolean;
  id: string = this.route.snapshot.params.id;
  dataSource: GamesDataSource;
  displayedColumns: string[] = ['date', 'type', 'playerOne', 'playerOnePoints', 'playerTwoPoints', 'playerTwo'];

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(
    private gameService: GameService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.isPlayerGames = this.player !== null ;
    this.setTotalGames();
    this.dataSource = new GamesDataSource(this.gameService);

    if (this.isPlayerGames) {
      this.dataSource.loadPlayerGames(this.id, 'date', false, 10, 0);
    } else {
      this.dataSource.loadMatchdayGames(this.id, 'date', false, 10, 0);
    }
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.loadGamesPage())
      )
      .subscribe();
  }

  loadGamesPage() {
    if (this.isPlayerGames) {
      this.dataSource.loadPlayerGames(
        this.id,
        this.sort.active,
        (this.sort.direction === 'desc'),
        this.paginator.pageSize,
        this.paginator.pageIndex
      );
    } else {
      this.dataSource.loadMatchdayGames(
        this.id,
        this.sort.active,
        (this.sort.direction === 'desc'),
        this.paginator.pageSize,
        this.paginator.pageIndex
      );
    }
  }

  setTotalGames() {
    if (this.isPlayerGames) {
      this.gameService.getNumberOfPlayerGames(this.id)
        .subscribe(res => {
          this.totalGames = res.totalCount;
        },
        error => {
          console.log(error);
        });
    } else {
      this.gameService.getNumberOfMatchdayGames(this.id)
        .subscribe(res => {
          this.totalGames = res.totalCount;
        },
        error => {
          console.log(error);
        });
    }
  }

  isWinner(game: Game): boolean {
    if (this.player === null) {
      return false;
    }

    if (this.player.id === game.playerOne.id) {
      return (game.playerOnePoints >= game.playerOneRequiredPoints) && (game.playerOnePoints > 0);
    }
    return (game.playerTwoPoints >= game.playerTwoRequiredPoints) && (game.playerTwoPoints > 0);
  }

  getType(type: string) {
    switch (type) {
      case '7': {
        return 'gameType.eightBall.long';
      }
      case '9': {
        return 'gameType.nineBall.long';
      }
      case '8': {
        return 'gameType.tenBall.long';
      }
      case '100': {
        return 'gameType.straightPool.long';
      }
    }
  }

}
